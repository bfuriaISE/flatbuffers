/*
 * Copyright 2014 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// independent from idl_parser, since this code is not needed for most clients

#include "flatbuffers/flatbuffers.h"
#include "flatbuffers/idl.h"
#include "flatbuffers/util.h"
#include <algorithm>

namespace flatbuffers {

// Convert an underscore_based_indentifier in to camelCase.
// Also uppercases the first character if first is true.
std::string MakeCamel(const std::string &in, bool first) {
  std::string s;
  for (size_t i = 0; i < in.length(); i++) {
    if (!i && first != (isupper(in[i]) != 0)) {
      s += static_cast<char>(first ? toupper(in[0]) : tolower(in[0]));
    } else if (in[i] == '_' && i + 1 < in.length()) {
      s += static_cast<char>(toupper(in[++i]));
    } else {
      s += in[i];
    }
  }
  return s;
}

struct CommentConfig {
  const char *first_line;
  const char *content_line_prefix;
  const char *last_line;
};

// Generate a documentation comment, if available.
void GenComment(const std::vector<std::string> &dc, std::string *code_ptr,
                const CommentConfig *config, const char *prefix) {
  if (dc.begin() == dc.end()) {
    // Don't output empty comment blocks with 0 lines of comment content.
    return;
  }

  std::string &code = *code_ptr;
  if (config != nullptr && config->first_line != nullptr) {
    code += std::string(prefix) + std::string(config->first_line) + "\n";
  }
  std::string line_prefix = std::string(prefix) +
      ((config != nullptr && config->content_line_prefix != nullptr) ?
       config->content_line_prefix : "///");
  for (auto it = dc.begin();
       it != dc.end();
       ++it) {
    code += line_prefix + *it + "\n";
  }
  if (config != nullptr && config->last_line != nullptr) {
    code += std::string(prefix) + std::string(config->last_line) + "\n";
  }
}

// These arrays need to correspond to the IDLOptions::k enum.

struct LanguageParameters {
  IDLOptions::Language language;
  // Whether function names in the language typically start with uppercase.
  bool first_camel_upper;
  const char *file_extension;
  const char *string_type;
  const char *bool_type;
  const char *open_curly;
  const char *const_decl;
  const char *unsubclassable_decl;
  const char *enum_decl;
  const char *enum_separator;
  const char *getter_prefix;
  const char *getter_suffix;
  const char *inheritance_marker;
  const char *namespace_ident;
  const char *namespace_begin;
  const char *namespace_end;
  const char *set_bb_byteorder;
  const char *get_bb_position;
  const char *get_fbb_offset;
  const char *includes;
  CommentConfig comment_config;
};

LanguageParameters language_parameters[] = {
  {
    IDLOptions::kJava,
    false,
    ".java",
    "String",
    "boolean ",
    " {\n",
    " final ",
    "final ",
    "final class ",
    ";\n",
    "()",
    "",
    " extends ",
    "package ",
    ";",
    "",
    "_bb.order(ByteOrder.LITTLE_ENDIAN); ",
    "position()",
    "offset()",
    "import java.nio.*;\nimport java.lang.*;\nimport java.util.*;\n"
      "import com.google.flatbuffers.*;\n\n@SuppressWarnings(\"unused\")\n",
    {
      "/**",
      " *",
      " */",
    },
  },
  {
    IDLOptions::kCSharp,
    true,
    ".cs",
    "string",
    "bool ",
    "\n{\n",
    " readonly ",
    "sealed ",
    "enum ",
    ",\n",
    " { get",
    "} ",
    " : ",
    "namespace ",
    "\n{",
    "\n}\n",
    "",
    "Position",
    "Offset",
    "using System;\nusing System.Collections.Generic;\nusing FlatBuffers;\n\n",
    {
      nullptr,
      "///",
      nullptr,
    },
  },
  // TODO: add Go support to the general generator.
  // WARNING: this is currently only used for generating make rules for Go.
  {
    IDLOptions::kGo,
    true,
    ".go",
    "string",
    "bool ",
    "\n{\n",
    "const ",
    " ",
    "class ",
    ";\n",
    "()",
    "",
    "",
    "package ",
    "",
    "",
    "",
    "position()",
    "offset()",
    "import (\n\tflatbuffers \"github.com/google/flatbuffers/go\"\n)",
    {
      nullptr,
      "///",
      nullptr,
    },
  }
};

static_assert(sizeof(language_parameters) / sizeof(LanguageParameters) ==
              IDLOptions::kMAX,
              "Please add extra elements to the arrays above.");

static std::string FunctionStart(const LanguageParameters &lang, char upper) {
  return std::string() +
      (lang.language == IDLOptions::kJava
         ? static_cast<char>(tolower(upper))
         : upper);
}

static bool IsEnum(const Type& type) {
  return type.enum_def != nullptr && IsInteger(type.base_type);
}

// Ensure that a type is prefixed with its namespace whenever it is used
// outside of its namespace.
static std::string WrapInNameSpace(const Parser &parser, const Namespace *ns,
    const std::string &name) {
        if (parser.namespaces_.back() != ns) {
            std::string qualified_name;
            for (auto it = ns->components.begin();
                it != ns->components.end(); ++it) {
                    qualified_name += *it + ".";
            }
            return qualified_name + name;
        } else {
            return name;
        }
}

static std::string WrapInNameSpace(const Parser &parser,
    const Definition &def) {
        return WrapInNameSpace(parser, def.defined_namespace, def.name);
}

static std::string GenTypeBasic(const LanguageParameters &lang, const Parser &parser,
                                const Type &type,
                                bool enableLangOverrides) {
  static const char *gtypename[] = {
    #define FLATBUFFERS_TD(ENUM, IDLTYPE, CTYPE, JTYPE, GTYPE, NTYPE, PTYPE) \
        #JTYPE, #NTYPE, #GTYPE,
      FLATBUFFERS_GEN_TYPES(FLATBUFFERS_TD)
    #undef FLATBUFFERS_TD
  };

  if (enableLangOverrides) {
    if (lang.language == IDLOptions::kCSharp) {
      if (IsEnum(type)) return WrapInNameSpace(parser, *type.enum_def);
      if (type.base_type == BASE_TYPE_STRUCT) {
          return "Offset<" + WrapInNameSpace(parser, *type.struct_def) + ">";
      }
    }
  }

  return gtypename[type.base_type * IDLOptions::kMAX + lang.language];
}

static std::string GenTypeBasic(const LanguageParameters &lang, const Parser &parser,
                                const Type &type) {
  return GenTypeBasic(lang, parser, type, true);
}

static std::string GenTypeGet(const LanguageParameters &lang, const Parser &parser,
                              const Type &type);

static std::string GenTypePointer(const LanguageParameters &lang, const Parser &parser,
                                  const Type &type) {
  switch (type.base_type) {
    case BASE_TYPE_STRING:
      return lang.string_type;
    case BASE_TYPE_VECTOR:
      return GenTypeGet(lang, parser, type.VectorType());
    case BASE_TYPE_STRUCT:
      return WrapInNameSpace(parser, *type.struct_def);
    case BASE_TYPE_UNION:
      // Unions in C# use a generic Table-derived type for better type safety
      if (lang.language == IDLOptions::kCSharp) return "TTable";
      // fall through
    default:
      return "Table";
  }
}

static std::string GenTypeGet(const LanguageParameters &lang, const Parser &parser,
                              const Type &type) {
  return IsScalar(type.base_type)
    ? GenTypeBasic(lang, parser, type)
    : GenTypePointer(lang, parser, type);
}

// Find the destination type the user wants to receive the value in (e.g.
// one size higher signed types for unsigned serialized values in Java).
static Type DestinationType(const LanguageParameters &lang, const Parser &parser, const Type &type,
                            bool vectorelem) {
  if (lang.language != IDLOptions::kJava) return type;
  switch (type.base_type) {
    // We use int for both uchar/ushort, since that generally means less casting
    // than using short for uchar.
    case BASE_TYPE_UCHAR:  return Type(BASE_TYPE_INT);
    case BASE_TYPE_USHORT: return Type(BASE_TYPE_INT);
    case BASE_TYPE_UINT:   return Type(BASE_TYPE_LONG);
    case BASE_TYPE_VECTOR:
      if (vectorelem)
        return DestinationType(lang, parser, type.VectorType(), vectorelem);
      // else fall thru:
    default: return type;
  }
}

static std::string GenOffsetType(const LanguageParameters &lang, const Parser &parser,
                                 const StructDef &struct_def) {
  if(lang.language == IDLOptions::kCSharp) {
    return "Offset<" + WrapInNameSpace(parser, struct_def) + ">";
  } else {
    return "int";
  }
}

static std::string GenOffsetConstruct(const LanguageParameters &lang, const Parser &parser,
                                      const StructDef &struct_def,
                                      const std::string &variable_name)
{
  if(lang.language == IDLOptions::kCSharp) {
    return "new Offset<" + WrapInNameSpace(parser, struct_def) + ">(" + variable_name + ")";
  }
  return variable_name;
}

static std::string GenVectorOffsetType(const LanguageParameters &lang) {
  if(lang.language == IDLOptions::kCSharp) {
    return "VectorOffset";
  } else {
    return "int";
  }
}

// Generate destination type name
static std::string GenTypeNameDest(const LanguageParameters &lang, const Parser &parser, const Type &type)
{
  return GenTypeGet(lang, parser, DestinationType(lang, parser, type, true));
}

// Mask to turn serialized value into destination type value.
static std::string DestinationMask(const LanguageParameters &lang,
                                   const Type &type, bool vectorelem) {
  if (lang.language != IDLOptions::kJava) return "";
  switch (type.base_type) {
    case BASE_TYPE_UCHAR:  return " & 0xFF";
    case BASE_TYPE_USHORT: return " & 0xFFFF";
    case BASE_TYPE_UINT:   return " & 0xFFFFFFFFL";
    case BASE_TYPE_VECTOR:
      if (vectorelem)
        return DestinationMask(lang, type.VectorType(), vectorelem);
      // else fall thru:
    default: return "";
  }
}

// Casts necessary to correctly read serialized data
static std::string DestinationCast(const LanguageParameters &lang, const Parser &parser,
                                   const Type &type) {
  if (type.base_type == BASE_TYPE_VECTOR) {
    return DestinationCast(lang, parser, type.VectorType());
  } else {
    switch (lang.language) {
    case IDLOptions::kJava:
      // Cast necessary to correctly read serialized unsigned values.
      if (type.base_type == BASE_TYPE_UINT) return "(long)";
      break;

    case IDLOptions::kCSharp:
      // Cast from raw integral types to enum.
      if (IsEnum(type)) return "(" + WrapInNameSpace(parser, *type.enum_def) + ")";
      break;

    default:
      break;
    }
  }
  return "";
}

// Cast statements for mutator method parameters.
// In Java, parameters representing unsigned numbers need to be cast down to their respective type.
// For example, a long holding an unsigned int value would be cast down to int before being put onto the buffer.
// In C#, one cast directly cast an Enum to its underlying type, which is essential before putting it onto the buffer.
static std::string SourceCast(const LanguageParameters &lang, const Parser &parser,
                              const Type &type,
                              bool castFromDest) {
  if (type.base_type == BASE_TYPE_VECTOR) {
    return SourceCast(lang, parser, type.VectorType(), castFromDest);
  } else {
    switch (lang.language) {
      case IDLOptions::kJava:
        if (castFromDest) {
          if (type.base_type == BASE_TYPE_UINT) return "(int)";
          else if (type.base_type == BASE_TYPE_USHORT) return "(short)";
          else if (type.base_type == BASE_TYPE_UCHAR) return "(byte)";
        }
        break;
      case IDLOptions::kCSharp:
        if (IsEnum(type)) return "(" + GenTypeBasic(lang, parser, type, false) + ")";
        break;
      default:
        break;
    }
  }
  return "";
}

static std::string SourceCast(const LanguageParameters &lang, const Parser &parser,
                              const Type &type) {
  return SourceCast(lang, parser, type, true);
}

static std::string SourceCastBasic(const LanguageParameters &lang, const Parser &parser,
                                   const Type &type,
                                   bool castFromDest) {
  return IsScalar(type.base_type) ? SourceCast(lang, parser, type, castFromDest) : "";
}

static std::string SourceCastBasic(const LanguageParameters &lang, const Parser &parser,
                                   const Type &type) {
  return SourceCastBasic(lang, parser, type, true);
}


static std::string GenEnumDefaultValue(const Parser &parser, const Value &value) {
  auto enum_def = value.type.enum_def;
  auto vec = enum_def->vals.vec;
  auto default_value = StringToInt(value.constant.c_str());

  auto result = value.constant;
  for (auto it = vec.begin(); it != vec.end(); ++it) {
    auto enum_val = **it;
    if (enum_val.value == default_value) {
      result = WrapInNameSpace(parser, *enum_def) + "." + enum_val.name;
      break;
    }
  }

  return result;
}

static std::string GenDefaultValue(const LanguageParameters &lang, const Parser &parser,
                                   const Value &value, bool enableLangOverrides) {
  if (enableLangOverrides) {
    // handles both enum case and vector of enum case
    if (lang.language == IDLOptions::kCSharp &&
        value.type.enum_def != nullptr &&
        value.type.base_type != BASE_TYPE_UNION) {
      return GenEnumDefaultValue(parser, value);
    }
  }
  switch (value.type.base_type) {
    case BASE_TYPE_FLOAT: return value.constant + "f";
    case BASE_TYPE_BOOL: return value.constant == "0" ? "false" : "true";
    default: return value.constant;
  }
}

static std::string GenDefaultValue(const LanguageParameters &lang, const Parser &parser,
                                   const Value &value) {
  return GenDefaultValue(lang, parser, value, true);
}

static std::string GenDefaultValueBasic(const LanguageParameters &lang, const Parser &parser,
                                        const Value &value, bool enableLangOverrides) {
  if (!IsScalar(value.type.base_type)) {
    if (enableLangOverrides) {
      if (lang.language == IDLOptions::kCSharp) {
        switch (value.type.base_type) {
        case BASE_TYPE_STRING:
          return "default(StringOffset)";
        case BASE_TYPE_STRUCT:
          return "default(Offset<" + WrapInNameSpace(parser, *value.type.struct_def) + ">)";
        case BASE_TYPE_VECTOR:
          return "default(VectorOffset)";
        default:
          break;
        }
      }
    }
    return "0";
  }
  return GenDefaultValue(lang, parser, value, enableLangOverrides);
}

static std::string GenDefaultValueBasic(const LanguageParameters &lang, const Parser &parser,
                                        const Value &value) {
  return GenDefaultValueBasic(lang, parser, value, true);
}

static void GenEnum(const LanguageParameters &lang, const Parser &parser, EnumDef &enum_def,
                    std::string *code_ptr) {
  std::string &code = *code_ptr;
  if (enum_def.generated) return;

  // Generate enum definitions of the form:
  // public static (final) int name = value;
  // In Java, we use ints rather than the Enum feature, because we want them
  // to map directly to how they're used in C/C++ and file formats.
  // That, and Java Enums are expensive, and not universally liked.
  GenComment(enum_def.doc_comment, code_ptr, &lang.comment_config);
  code += std::string("public ") + lang.enum_decl + enum_def.name;
  if (lang.language == IDLOptions::kCSharp) {
    code += lang.inheritance_marker + GenTypeBasic(lang, parser, enum_def.underlying_type, false);
  }
  code += lang.open_curly;
  if (lang.language == IDLOptions::kJava) {
    code += "  private " + enum_def.name + "() { }\n";
  }
  for (auto it = enum_def.vals.vec.begin();
       it != enum_def.vals.vec.end();
       ++it) {
    auto &ev = **it;
    GenComment(ev.doc_comment, code_ptr, &lang.comment_config, "  ");
    if (lang.language != IDLOptions::kCSharp) {
      code += "  public static";
      code += lang.const_decl;
      code += GenTypeBasic(lang, parser, enum_def.underlying_type, false);
    }
    code += " " + ev.name + " = ";
    code += NumToString(ev.value);
    code += lang.enum_separator;
  }

  // Generate a generate string table for enum values.
  // We do not do that for C# where this functionality is native.
  if (lang.language != IDLOptions::kCSharp) {
    // Problem is, if values are very sparse that could generate really big
    // tables. Ideally in that case we generate a map lookup instead, but for
    // the moment we simply don't output a table at all.
    auto range = enum_def.vals.vec.back()->value -
      enum_def.vals.vec.front()->value + 1;
    // Average distance between values above which we consider a table
    // "too sparse". Change at will.
    static const int kMaxSparseness = 5;
    if (range / static_cast<int64_t>(enum_def.vals.vec.size()) < kMaxSparseness) {
      code += "\n  private static";
      code += lang.const_decl;
      code += lang.string_type;
      code += "[] names = { ";
      auto val = enum_def.vals.vec.front()->value;
      for (auto it = enum_def.vals.vec.begin();
        it != enum_def.vals.vec.end();
        ++it) {
        while (val++ != (*it)->value) code += "\"\", ";
        code += "\"" + (*it)->name + "\", ";
      }
      code += "};\n\n";
      code += "  public static ";
      code += lang.string_type;
      code += " " + MakeCamel("name", lang.first_camel_upper);
      code += "(int e) { return names[e";
      if (enum_def.vals.vec.front()->value)
        code += " - " + enum_def.vals.vec.front()->name;
      code += "]; }\n";
    }
  }

  // Close the class
  code += "};\n\n";
}

// Returns the function name that is able to read a value of the given type.
static std::string GenGetter(const LanguageParameters &lang, const Parser &parser,
                             const Type &type) {
  switch (type.base_type) {
    case BASE_TYPE_STRING: return "__string";
    case BASE_TYPE_STRUCT: return "__struct";
    case BASE_TYPE_UNION:  return "__union";
    case BASE_TYPE_VECTOR: return GenGetter(lang, parser, type.VectorType());
    default: {
      std::string getter = "bb." + FunctionStart(lang, 'G') + "et";
      if (type.base_type == BASE_TYPE_BOOL) {
        getter = "0!=" + getter;
      } else if (GenTypeBasic(lang, parser, type, false) != "byte") {
        getter += MakeCamel(GenTypeBasic(lang, parser, type, false));
      }
      return getter;
    }
  }
}

// Direct mutation is only allowed for scalar fields.
// Hence a setter method will only be generated for such fields.
static std::string GenSetter(const LanguageParameters &lang, const Parser &parser,
                             const Type &type) {
  if (IsScalar(type.base_type)) {
    std::string setter = "bb." + FunctionStart(lang, 'P') + "ut";
    if (GenTypeBasic(lang, parser, type, false) != "byte" &&
        type.base_type != BASE_TYPE_BOOL) {
      setter += MakeCamel(GenTypeBasic(lang, parser, type, false));
    }
    return setter;
  } else {
    return "";
  }
}

// Returns the method name for use with add/put calls.
static std::string GenMethod(const LanguageParameters &lang, const Parser &parser, const Type &type) {
  return IsScalar(type.base_type)
    ? MakeCamel(GenTypeBasic(lang, parser, type, false))
    : (IsStruct(type) ? "Struct" : "Offset");
}

// Recursively generate arguments for a constructor, to deal with nested
// structs.
static void GenStructArgs(const LanguageParameters &lang, const Parser &parser,
                          const StructDef &struct_def,
                          std::string *code_ptr, const char *nameprefix) {
  std::string &code = *code_ptr;
  for (auto it = struct_def.fields.vec.begin();
       it != struct_def.fields.vec.end();
       ++it) {
    auto &field = **it;
    if (IsStruct(field.value.type)) {
      // Generate arguments for a struct inside a struct. To ensure names
      // don't clash, and to make it obvious these arguments are constructing
      // a nested struct, prefix the name with the field name.
      GenStructArgs(lang, parser, *field.value.type.struct_def, code_ptr,
                    (nameprefix + (field.name + "_")).c_str());
    } else {
      code += ", ";
      code += GenTypeBasic(lang, parser, DestinationType(lang, parser, field.value.type, false));
      code += " ";
      code += nameprefix;
      code += MakeCamel(field.name, lang.first_camel_upper);
    }
  }
}

// Recusively generate struct construction statements of the form:
// builder.putType(name);
// and insert manual padding.
static void GenStructBody(const LanguageParameters &lang, const Parser &parser,
                          const StructDef &struct_def,
                          std::string *code_ptr, const char *nameprefix) {
  std::string &code = *code_ptr;
  code += "    builder." + FunctionStart(lang, 'P') + "rep(";
  code += NumToString(struct_def.minalign) + ", ";
  code += NumToString(struct_def.bytesize) + ");\n";
  for (auto it = struct_def.fields.vec.rbegin();
       it != struct_def.fields.vec.rend(); ++it) {
    auto &field = **it;
    if (field.padding) {
      code += "    builder." + FunctionStart(lang, 'P') + "ad(";
      code += NumToString(field.padding) + ");\n";
    }
    if (IsStruct(field.value.type)) {
      GenStructBody(lang, parser, *field.value.type.struct_def, code_ptr,
                    (nameprefix + (field.name + "_")).c_str());
    } else {
      code += "    builder." + FunctionStart(lang, 'P') + "ut";
      code += GenMethod(lang, parser, field.value.type) + "(";
      code += SourceCast(lang, parser, field.value.type);
      auto argname = nameprefix + MakeCamel(field.name, lang.first_camel_upper);
      code += argname;
      code += ");\n";
    }
  }
}

static void GenStruct(const LanguageParameters &lang, const Parser &parser,
                      StructDef &struct_def, std::string *code_ptr) {
  if (struct_def.generated) return;
  std::string &code = *code_ptr;

  // Generate a struct accessor class, with methods of the form:
  // public type name() { return bb.getType(i + offset); }
  // or for tables of the form:
  // public type name() {
  //   int o = __offset(offset); return o != 0 ? bb.getType(o + i) : default;
  // }
  GenComment(struct_def.doc_comment, code_ptr, &lang.comment_config);
  code += "public ";
  if (lang.language == IDLOptions::kCSharp &&
      struct_def.attributes.Lookup("csharp_partial")) {
    // generate a partial class for this C# struct/table
    code += "partial ";
  }
  else {
    code += lang.unsubclassable_decl;
  }
  code += "class " + struct_def.name + lang.inheritance_marker;
  code += struct_def.fixed ? "Struct" : "Table";
  code += " {\n";
  if (!struct_def.fixed) {
    // Generate a special accessor for the table that when used as the root
    // of a FlatBuffer
    std::string method_name = FunctionStart(lang, 'G') + "etRootAs" + struct_def.name;
    std::string method_signature = "  public static " + struct_def.name + " " + method_name;

    // create convenience method that doesn't require an existing object
    code += method_signature + "(ByteBuffer _bb) ";
    code += "{ return " + method_name + "(_bb, new " + struct_def.name+ "()); }\n";

    // create method that allows object reuse
    code += method_signature + "(ByteBuffer _bb, " + struct_def.name + " obj) { ";
    code += lang.set_bb_byteorder;
    code += "return (obj.__init(_bb." + FunctionStart(lang, 'G');
    code += "etInt(_bb.";
    code += lang.get_bb_position;
    code += ") + _bb.";
    code += lang.get_bb_position;
    code += ", _bb)); }\n";

    if (lang.language == IDLOptions::kCSharp) {
      std::string typeName = "ByteBufferSegment";
      std::string paramName = "bbs";
      std::string paramDecl = typeName + " " + paramName;

      // create convenience method that doesn't require an existing object
      code += method_signature + "(" + paramDecl + ") ";
      code += "{ return " + method_name + "(" + paramName;
      code += ", new " + struct_def.name + "()); }\n";

      std::string byteBuffer = paramName + ".ByteBuffer";
      std::string segmentOffset = paramName + ".Offset";

      // create method that allows object reuse
      code += method_signature + "(" + paramDecl + ", " + struct_def.name + " obj) { ";
      code += "return (obj.__init(" + byteBuffer + "." + FunctionStart(lang, 'G');
      code += "etInt(" + segmentOffset + ") + " + segmentOffset;
      code += ", " + byteBuffer + ")); }\n";
    }

    if (parser.root_struct_def_ == &struct_def) {
      if (parser.file_identifier_.length()) {
        // Check if a buffer has the identifier.
        code += "  public static ";
        code += lang.bool_type + struct_def.name;
        code += "BufferHasIdentifier(ByteBuffer _bb) { return ";
        code += "__has_identifier(_bb, \"" + parser.file_identifier_;
        code += "\"); }\n";
      }
    }
  }
  // Generate the __init method that sets the field in a pre-existing
  // accessor object. This is to allow object reuse.
  code += "  public " + struct_def.name;
  code += " __init(int _i, ByteBuffer _bb) ";
  code += "{ bb_pos = _i; bb = _bb; return this; }\n\n";
  for (auto it = struct_def.fields.vec.begin();
       it != struct_def.fields.vec.end();
       ++it) {
    auto &field = **it;
    if (field.deprecated) continue;
    GenComment(field.doc_comment, code_ptr, &lang.comment_config, "  ");
    std::string type_name = GenTypeGet(lang, parser, field.value.type);
    std::string type_name_dest = GenTypeNameDest(lang, parser, field.value.type);
    std::string dest_mask = DestinationMask(lang, field.value.type, true);
    std::string dest_cast = DestinationCast(lang, parser, field.value.type);
    std::string src_cast = SourceCast(lang, parser, field.value.type);
    std::string method_start = "  public " + type_name_dest + " " +
                               MakeCamel(field.name, lang.first_camel_upper);

    // Most field accessors need to retrieve and test the field offset first,
    // this is the prefix code for that:
    auto offset_prefix = " { int o = __offset(" +
      NumToString(field.value.offset) +
      "); return o != 0 ? ";
    // Generate the accessors that don't do object reuse.
    if (field.value.type.base_type == BASE_TYPE_STRUCT) {
      // Calls the accessor that takes an accessor object with a new object.
      if (lang.language == IDLOptions::kCSharp) {
        code += method_start + " { get { return Get";
        code += MakeCamel(field.name, lang.first_camel_upper);
        code += "(new ";
        code += type_name + "()); } }\n";
        method_start = "  public " + type_name_dest + " Get" + MakeCamel(field.name, lang.first_camel_upper);
      }
      else {
        code += method_start + "() { return ";
        code += MakeCamel(field.name, lang.first_camel_upper);
        code += "(new ";
        code += type_name + "()); }\n";
      }
    } else if (field.value.type.base_type == BASE_TYPE_VECTOR &&
               field.value.type.element == BASE_TYPE_STRUCT) {
      // Accessors for vectors of structs also take accessor objects, this
      // generates a variant without that argument.
      if (lang.language == IDLOptions::kCSharp) {
        method_start = "  public " + type_name_dest + " Get" + MakeCamel(field.name, lang.first_camel_upper);
        code += method_start + "(int j) { return Get";
      } else {
        code += method_start + "(int j) { return ";
      }
      code += MakeCamel(field.name, lang.first_camel_upper);
      code += "(new ";
      code += type_name + "(), j); }\n";
    } else if (field.value.type.base_type == BASE_TYPE_VECTOR) {
      if (lang.language == IDLOptions::kCSharp) {
        method_start = "  public " + type_name_dest + " Get" + MakeCamel(field.name, lang.first_camel_upper);
      }
    } else if (field.value.type.base_type == BASE_TYPE_UNION) {
      if (lang.language == IDLOptions::kCSharp) {
        // union types in C# use generic Table-derived type for better type safety
        method_start = "  public " + type_name_dest + " Get" + MakeCamel(field.name, lang.first_camel_upper) + "<TTable>";
        offset_prefix = " where TTable : Table" + offset_prefix;
        type_name = type_name_dest;
      }
    }
    std::string getter = dest_cast + GenGetter(lang, parser, field.value.type);
    code += method_start;
    std::string default_cast = "";
    // only create default casts for c# scalars or vectors of scalars
    if (lang.language == IDLOptions::kCSharp &&
        (IsScalar(field.value.type.base_type) ||
         (field.value.type.base_type == BASE_TYPE_VECTOR && IsScalar(field.value.type.element)))) {
      // For scalars, default value will be returned by GetDefaultValue(). If the scalar is an enum, GetDefaultValue()
      // returns an actual c# enum that doesn't need to be casted. However, default values for enum elements of
      // vectors are integer literals ("0") and are still casted for clarity.
      if (field.value.type.enum_def == nullptr || field.value.type.base_type == BASE_TYPE_VECTOR) {
          default_cast = "(" + type_name_dest + ")";
      }
    }
    std::string member_suffix = "";
    if (IsScalar(field.value.type.base_type)) {
      code += lang.getter_prefix;
      member_suffix = lang.getter_suffix;
      if (struct_def.fixed) {
        code += " { return " + getter;
        code += "(bb_pos + " + NumToString(field.value.offset) + ")";
        code += dest_mask;
      } else {
        code += offset_prefix + getter;
        code += "(o + bb_pos)" + dest_mask + " : " + default_cast;
        code += GenDefaultValue(lang, parser, field.value);
      }
    } else {
      switch (field.value.type.base_type) {
        case BASE_TYPE_STRUCT:
          code += "(" + type_name + " obj";
          if (struct_def.fixed) {
            code += ") { return obj.__init(bb_pos + ";
            code += NumToString(field.value.offset) + ", bb)";
          } else {
            code += ")";
            code += offset_prefix;
            code += "obj.__init(";
            code += field.value.type.struct_def->fixed
                      ? "o + bb_pos"
                      : "__indirect(o + bb_pos)";
            code += ", bb) : null";
          }
          break;
        case BASE_TYPE_STRING:
          code += lang.getter_prefix;
          member_suffix = lang.getter_suffix;
          code += offset_prefix + getter + "(o + bb_pos) : null";
          break;
        case BASE_TYPE_VECTOR: {
          auto vectortype = field.value.type.VectorType();
          code += "(";
          if (vectortype.base_type == BASE_TYPE_STRUCT) {
            code += type_name + " obj, ";
            getter = "obj.__init";
          }
          code += "int j)" + offset_prefix + getter +"(";
          auto index = "__vector(o) + j * " +
                       NumToString(InlineSize(vectortype));
          if (vectortype.base_type == BASE_TYPE_STRUCT) {
            code += vectortype.struct_def->fixed
                      ? index
                      : "__indirect(" + index + ")";
            code += ", bb";
          } else {
            code += index;
          }
          code += ")" + dest_mask + " : ";

          code += field.value.type.element == BASE_TYPE_BOOL ? "false" :
            (IsScalar(field.value.type.element) ? default_cast + "0" : "null");
          break;
        }
        case BASE_TYPE_UNION:
          code += "(" + type_name + " obj)" + offset_prefix + getter;
          code += "(obj, o) : null";
          break;
        default:
          assert(0);
      }
    }
    code += "; ";
    code += member_suffix;
    code += "}\n";
    if (field.value.type.base_type == BASE_TYPE_VECTOR) {
      code += "  public int " + MakeCamel(field.name, lang.first_camel_upper);
      code += "Length";
      code += lang.getter_prefix;
      code += offset_prefix;
      code += "__vector_len(o) : 0; ";
      code += lang.getter_suffix;
      code += "}\n";
    }
    // Generate a ByteBuffer accessor for strings & vectors of scalars.
    if ((field.value.type.base_type == BASE_TYPE_VECTOR &&
         IsScalar(field.value.type.VectorType().base_type)) ||
         field.value.type.base_type == BASE_TYPE_STRING) {
      switch (lang.language) {
        case IDLOptions::kJava:
          code += "  public ByteBuffer ";
          code += MakeCamel(field.name, lang.first_camel_upper);
          code += "AsByteBuffer() { return __vector_as_bytebuffer(";
          code += NumToString(field.value.offset) + ", ";
          code += NumToString(field.value.type.base_type == BASE_TYPE_STRING ? 1 :
                              InlineSize(field.value.type.VectorType()));
          code += "); }\n";
          break;
        case IDLOptions::kCSharp:
          code += "  public ArraySegment<byte>? Get";
          code += MakeCamel(field.name, lang.first_camel_upper);
          code += "Bytes() { return __vector_as_arraysegment(";
          code += NumToString(field.value.offset);
          code += "); }\n";
          code += "  public ByteBufferSegment? Get";
          code += MakeCamel(field.name, lang.first_camel_upper);
          code += "BufferSegment() { return __vector_as_bytebuffersegment(";
          code += NumToString(field.value.offset);
          code += "); }\n";
          break;
        default:
          break;
      }
    }

    // generate object accessors if is nested_flatbuffer
    auto nested = field.attributes.Lookup("nested_flatbuffer");
    if (nested) {
      auto nested_qualified_name = nested->constant;
      // first attempt lookup using attribute value
      auto nested_type = parser.structs_.Lookup(nested_qualified_name);
      // if not found, try qualifying the name using known namespaces
      if (nested_type == nullptr) {
        for (auto namespaceItr = parser.namespaces_.rbegin(); 
             namespaceItr != parser.namespaces_.rend() && nested_type == nullptr; 
             ++namespaceItr) {
          nested_qualified_name = (*namespaceItr)->GetFullyQualifiedName(nested->constant);
          nested_type = parser.structs_.Lookup(nested_qualified_name);
        }
      }
      // generate helper if we can find the specified flatbuffer
      if (nested_type != nullptr) {
        auto nestedMethodName = MakeCamel(field.name, lang.first_camel_upper)
          + "As" + nested_type->name;
        auto getNestedMethodName = nestedMethodName;
        if (lang.language == IDLOptions::kCSharp) {
          getNestedMethodName = "Get" + nestedMethodName;
        }
        auto nested_type_name = WrapInNameSpace(parser, *nested_type);
        code += "  public " + nested_type_name + " ";
        code += nestedMethodName + "() { return ";
        code += getNestedMethodName + "(new " + nested_type_name + "()); }\n";
        code += "  public " + nested_type_name + " " + getNestedMethodName;
        code += "(" + nested_type_name + " obj) { ";
        code += "int o = __offset(" + NumToString(field.value.offset) + "); ";
        code += "return o != 0 ? obj.__init(__indirect(__vector(o)), bb) : null; }\n";
      }
    }

    // generate mutators for scalar fields or vectors of scalars
    if (parser.opts.mutable_buffer) {
      auto underlying_type = field.value.type.base_type == BASE_TYPE_VECTOR
                    ? field.value.type.VectorType()
                    : field.value.type;
      // boolean parameters have to be explicitly converted to byte representation
      auto setter_parameter = underlying_type.base_type == BASE_TYPE_BOOL ? "(byte)(" + field.name + " ? 1 : 0)" : field.name;
      auto mutator_prefix = MakeCamel("mutate", lang.first_camel_upper);
      //a vector mutator also needs the index of the vector element it should mutate
      auto mutator_params = (field.value.type.base_type == BASE_TYPE_VECTOR ? "(int j, " : "(") +
                            GenTypeNameDest(lang, parser, underlying_type) + " " +
                            field.name + ") { ";
      auto setter_index = field.value.type.base_type == BASE_TYPE_VECTOR
                    ? "__vector(o) + j * " + NumToString(InlineSize(underlying_type))
                    : (struct_def.fixed ? "bb_pos + " + NumToString(field.value.offset) : "o + bb_pos");
      if (IsScalar(field.value.type.base_type) ||
          (field.value.type.base_type == BASE_TYPE_VECTOR &&
          IsScalar(field.value.type.VectorType().base_type))) {
        code += "  public ";
        code += struct_def.fixed ? "void " : lang.bool_type;
        code += mutator_prefix + MakeCamel(field.name, true);
        code += mutator_params;
        if (struct_def.fixed) {
          code += GenSetter(lang, parser, underlying_type) + "(" + setter_index + ", ";
          code += src_cast + setter_parameter + "); }\n";
        } else {
          code += "int o = __offset(" + NumToString(field.value.offset) + ");";
          code += " if (o != 0) { " + GenSetter(lang, parser, underlying_type);
          code += "(" + setter_index + ", " + src_cast + setter_parameter + "); return true; } else { return false; } }\n";
        }
      }
    }

    // generate is specified properties for Scalar types within tables
    if (IsScalar(field.value.type.base_type) && !struct_def.fixed){
      code += "  public bool Is" + MakeCamel(field.name, true) + "Specified { get { return ";
      code += "__offset(" + NumToString(field.value.offset) + ")" + " != 0";
      code += "; } }\n";
    }
  }
  code += "\n";
  if (struct_def.fixed) {
    // create a struct constructor function
    code += "  public static " + GenOffsetType(lang, parser, struct_def) + " ";
    code += FunctionStart(lang, 'C') + "reate";
    code += struct_def.name + "(FlatBufferBuilder builder";
    GenStructArgs(lang, parser, struct_def, code_ptr, "");
    code += ") {\n";
    GenStructBody(lang, parser, struct_def, code_ptr, "");
    code += "    return ";
    code += GenOffsetConstruct(lang, parser, struct_def, "builder." + std::string(lang.get_fbb_offset));
    code += ";\n  }\n";
  } else {
    // Generate a method that creates a table in one go. This is only possible
    // when the table has no struct fields, since those have to be created
    // inline, and there's no way to do so in Java.
    bool has_no_struct_fields = true;
    int num_fields = 0;
    for (auto it = struct_def.fields.vec.begin();
         it != struct_def.fields.vec.end(); ++it) {
      auto &field = **it;
      if (field.deprecated) continue;
      if (IsStruct(field.value.type)) {
        has_no_struct_fields = false;
      } else {
        num_fields++;
      }
    }
    if (has_no_struct_fields && num_fields) {
      // Generate a table constructor of the form:
      // public static int createName(FlatBufferBuilder builder, args...)
      code += "  public static " + GenOffsetType(lang, parser, struct_def) + " ";
      code += FunctionStart(lang, 'C') + "reate" + struct_def.name;
      code += "(FlatBufferBuilder builder";
      for (auto it = struct_def.fields.vec.begin();
           it != struct_def.fields.vec.end(); ++it) {
        auto &field = **it;
        if (field.deprecated) continue;
        code += ",\n      ";
        code += GenTypeBasic(lang, parser, DestinationType(lang, parser, field.value.type, false));
        code += " ";
        code += field.name;
        if (!IsScalar(field.value.type.base_type)) code += "Offset";

        // Java doesn't have defaults, which means this method must always
        // supply all arguments, and thus won't compile when fields are added.
        if (lang.language != IDLOptions::kJava) {
          code += " = ";
          code += GenDefaultValueBasic(lang, parser, field.value);
        }
      }
      // add parameter to enable/disable vtable reuse
      if (lang.language == IDLOptions::kCSharp) {
        code += ",\n      bool enableVtableReuse = true";
      }
      code += ") {\n    builder.";
      code += FunctionStart(lang, 'S') + "tartObject(";
      code += NumToString(struct_def.fields.vec.size()) + ");\n";
      for (size_t size = struct_def.sortbysize ? sizeof(largest_scalar_t) : 1;
           size;
           size /= 2) {
        for (auto it = struct_def.fields.vec.rbegin();
             it != struct_def.fields.vec.rend(); ++it) {
          auto &field = **it;
          if (!field.deprecated &&
              (!struct_def.sortbysize ||
               size == SizeOf(field.value.type.base_type))) {
            code += "    " + struct_def.name + ".";
            code += FunctionStart(lang, 'A') + "dd";
            code += MakeCamel(field.name) + "(builder, " + field.name;
            if (!IsScalar(field.value.type.base_type)) code += "Offset";
            code += ");\n";
          }
        }
      }
      code += "    return " + struct_def.name + ".";
      code += FunctionStart(lang, 'E') + "nd" + struct_def.name;
      code += "(builder";
      if (lang.language == IDLOptions::kCSharp) {
        code += ", enableVtableReuse";
      }
      code += ");\n  }\n\n";
    }
    // Generate a set of static methods that allow table construction,
    // of the form:
    // public static void addName(FlatBufferBuilder builder, short name)
    // { builder.addShort(id, name, default); }
    // Unlike the Create function, these always work.
    code += "  public static void " + FunctionStart(lang, 'S') + "tart";
    code += struct_def.name;
    code += "(FlatBufferBuilder builder) { builder.";
    code += FunctionStart(lang, 'S') + "tartObject(";
    code += NumToString(struct_def.fields.vec.size()) + "); }\n";
    for (auto it = struct_def.fields.vec.begin();
         it != struct_def.fields.vec.end(); ++it) {
      auto &field = **it;
      if (field.deprecated) continue;
      code += "  public static void " + FunctionStart(lang, 'A') + "dd";
      code += MakeCamel(field.name);
      code += "(FlatBufferBuilder builder, ";
      code += GenTypeBasic(lang, parser, DestinationType(lang, parser, field.value.type, false));
      auto argname = MakeCamel(field.name, false);
      if (!IsScalar(field.value.type.base_type)) argname += "Offset";
      code += " " + argname + ") { builder." + FunctionStart(lang, 'A') + "dd";
      code += GenMethod(lang, parser, field.value.type) + "(";
      code += NumToString(it - struct_def.fields.vec.begin()) + ", ";
      code += SourceCastBasic(lang, parser, field.value.type);
      code += argname;
      if(!IsScalar(field.value.type.base_type) && field.value.type.base_type != BASE_TYPE_UNION && lang.language == IDLOptions::kCSharp) {
        code += ".Value";
      }
      code += ", " + GenDefaultValue(lang, parser, field.value, false);
      code += "); }\n";
      if (field.value.type.base_type == BASE_TYPE_VECTOR) {
        auto vector_type = field.value.type.VectorType();
        auto alignment = InlineAlignment(vector_type);
        auto elem_size = InlineSize(vector_type);
        if (!IsStruct(vector_type)) {
          std::string elem_type_name = GenTypeBasic(lang, parser, vector_type);
          std::vector<std::tuple<std::string, std::string, bool>> list_param_info_vec;

          list_param_info_vec.push_back(std::make_tuple(elem_type_name + "[]", std::string("Length"), true));
          if (lang.language == IDLOptions::kCSharp) {
            list_param_info_vec.push_back(std::make_tuple("List<" + elem_type_name + ">", std::string("Count"), false));
            list_param_info_vec.push_back(std::make_tuple("IList<" + elem_type_name + ">", std::string("Count"), false));
          }

          for (auto itr = list_param_info_vec.begin(); itr != list_param_info_vec.end(); ++itr) {
            const std::string& list_type_name = std::get<0>(*itr);
            const std::string& list_length_prop_name = std::get<1>(*itr);
            bool is_array = std::get<2>(*itr);
            
            // Generate a method to create a vector from a Java array.
            code += "  public static " + GenVectorOffsetType(lang) + " " + FunctionStart(lang, 'C') + "reate";
            code += MakeCamel(field.name);
            code += "Vector(FlatBufferBuilder builder, ";
            code += list_type_name + " data) ";
            code += "{ ";

            std::string list_length_prop_name_camel = 
              MakeCamel(list_length_prop_name, lang.language == IDLOptions::kCSharp);
            std::string list_length_access = "data." + list_length_prop_name_camel;

            if (lang.language == IDLOptions::kCSharp 
                && IsScalar(vector_type.base_type) 
                && !IsEnum(vector_type) 
                && is_array) {
              code += "return builder.Create";
              code += GenMethod(lang, parser, vector_type);
              code += "Vector(data, 0, " + list_length_access + ");";
            } else {
              // Generate a method to create a vector from a Java array.
              code += "builder." + FunctionStart(lang, 'S') + "tartVector(";
              code += NumToString(elem_size);
              code += ", " + list_length_access + ", ";
              code += NumToString(alignment);
              code += "); for (int i = ";
              code += list_length_access + " - 1; i >= 0; i--) builder.";
              code += FunctionStart(lang, 'A') + "dd";
              code += GenMethod(lang, parser, vector_type);
              code += "(";
              code += SourceCastBasic(lang, parser, vector_type, false);
              code += "data[i]";
              if (lang.language == IDLOptions::kCSharp &&
                  (vector_type.base_type == BASE_TYPE_STRUCT || vector_type.base_type == BASE_TYPE_STRING))
                  code += ".Value";
              code += "); return ";
              code += "builder." + FunctionStart(lang, 'E') + "ndVector();";
            }
            code += " }\n";
          }
        }
        // Generate a method to start a vector, data to be added manually after.
        code += "  public static void " + FunctionStart(lang, 'S') + "tart";
        code += MakeCamel(field.name);
        code += "Vector(FlatBufferBuilder builder, int numElems) ";
        code += "{ builder." + FunctionStart(lang, 'S') + "tartVector(";
        code += NumToString(elem_size);
        code += ", numElems, " + NumToString(alignment);
        code += "); }\n";
      }
    }
    code += "  public static " + GenOffsetType(lang, parser, struct_def) + " ";
    code += FunctionStart(lang, 'E') + "nd" + struct_def.name;
    code += "(FlatBufferBuilder builder";
    if (lang.language == IDLOptions::kCSharp) {
      code += ", bool enableVtableReuse = true";
    }
    code += ") {\n    int o = builder.";
    code += FunctionStart(lang, 'E') + "ndObject(";
    if (lang.language == IDLOptions::kCSharp) {
      code += "enableVtableReuse";
    }
    code += ");\n";
    for (auto it = struct_def.fields.vec.begin();
         it != struct_def.fields.vec.end();
         ++it) {
      auto &field = **it;
      if (!field.deprecated && field.required) {
        code += "    builder." + FunctionStart(lang, 'R') + "equired(o, ";
        code += NumToString(field.value.offset);
        code += ");  // " + field.name + "\n";
      }
    }
    code += "    return " + GenOffsetConstruct(lang, parser, struct_def, "o") + ";\n  }\n";
    if (parser.root_struct_def_ == &struct_def) {
      code += "  public static void ";
      code += FunctionStart(lang, 'F') + "inish" + struct_def.name;
      code += "Buffer(FlatBufferBuilder builder, " + GenOffsetType(lang, parser, struct_def) + " offset) {";
      code += " builder." + FunctionStart(lang, 'F') + "inish(offset";
      if (lang.language == IDLOptions::kCSharp) {
        code += ".Value";
      }

      if (parser.file_identifier_.length())
        code += ", \"" + parser.file_identifier_ + "\"";
      code += "); }\n";
    }
  }
  code += "};\n\n";
}


static std::string GenTypePointerCsStruct(const LanguageParameters &lang, const Parser &parser,
                                          const Type &type) {
  if (type.base_type == BASE_TYPE_VECTOR) {
    Type vectorElemType = type.VectorType();
    std::string vectorElemTypeName =
      vectorElemType.base_type != BASE_TYPE_STRUCT 
        ? GenTypeGet(lang, parser, type.VectorType()) 
        : vectorElemType.struct_def->name;
    return MakeCamel(vectorElemTypeName) + "Vector";
  }

  std::string pointerTypeName = GenTypePointer(lang, parser, type);

  if (type.base_type == BASE_TYPE_STRUCT) {
    pointerTypeName += "Struct";
  }

  return pointerTypeName;
}

static std::string GenTypeGetCsStruct(const LanguageParameters &lang, const Parser &parser,
                                      const Type &type) {
  return IsScalar(type.base_type)
    ? GenTypeBasic(lang, parser, type)
    : GenTypePointerCsStruct(lang, parser, type);
}

static void GenFieldAccessors(const LanguageParameters &lang, const Parser &parser,
                             const StructDef& struct_def, const Value& value,
                             const Type &type, const std::string& accessor_name_root, 
                             std::string *code_ptr) {
  if (type.base_type == BASE_TYPE_UNION)
    return;

  std::string &code = *code_ptr;
  std::string type_name = GenTypeGetCsStruct(lang, parser, type);
  std::string dest_cast = DestinationCast(lang, parser, type);
  std::string src_cast = SourceCast(lang, parser, type);

  std::string return_type = type_name;
  if (type.base_type == BASE_TYPE_STRUCT && !struct_def.fixed
      || type.base_type == BASE_TYPE_VECTOR) {
    return_type += '?';
  }

  std::string method_name = MakeCamel(accessor_name_root, true);
  std::string method_start = "  public " + return_type + " " + method_name;
  std::string accessor_var_type_name = struct_def.fixed ? "BufferPosition" : "TableAccessor";
  std::string accessor_var_name = struct_def.fixed ? "_bufferPosition" : "_tableAccessor";

  std::string param_name = MakeCamel(accessor_name_root, false);
  if (IsScalar(type.base_type) || type.base_type == BASE_TYPE_STRING) {
    param_name += "Value";
  } else if (type.base_type == BASE_TYPE_VECTOR) {
    param_name += "Vector";
  } else {
    assert(type.struct_def != nullptr);
    if (type.struct_def->fixed)
      param_name += "Struct";
    else
      param_name += "Table";
  }

  code += method_start + " {";

  if (IsScalar(type.base_type) || type.base_type == BASE_TYPE_STRING) {
    std::string type_name_in_camel = MakeCamel(type_name);
    std::string value_accessor_method_suffix = 
      IsEnum(type) ? MakeCamel(GenTypeBasic(lang, parser, type, false)) : type_name_in_camel;
    std::string value_accessor_method = "Get" + value_accessor_method_suffix;
    std::string default_value_arg;
    if (!struct_def.fixed) {
      value_accessor_method += "FieldValue";
      if (IsScalar(type.base_type))
        default_value_arg = ", " + GenDefaultValue(lang, parser, value, false);
    }
    
    code += " get { return " + dest_cast + accessor_var_name + "." + value_accessor_method + "(" 
            + NumToString(value.offset) + default_value_arg + "); } }\n";

    if (IsScalar(type.base_type)) {
      code += "  public ";
      code += struct_def.fixed ? "void" : "bool";
      code += " Mutate" + method_name + "(" + type_name + " " + param_name + ") { ";
      if (!struct_def.fixed)
        code += "return ";
      code += accessor_var_name + ".";
      code += struct_def.fixed ? "Put" : "Mutate";
      code += value_accessor_method_suffix;
      if (!struct_def.fixed)
        code += "FieldValue";
      code += "(" + NumToString(value.offset) + ", " + src_cast + param_name + "); }\n";

      if (!struct_def.fixed) {
        code += "  public bool Is" + method_name + "Specified { get { return ";
        code += accessor_var_name + ".CheckField(" + NumToString(value.offset);
        code += "); } }\n";
      }
    }
  } else {
    std::string out_value_type = "BufferPosition";
    std::string out_value_var_name = "position";
    std::string out_value_var_decl = out_value_type + " " + out_value_var_name + ";";

    if (type.base_type == BASE_TYPE_STRUCT && struct_def.fixed) {
      std::string create_buf_pos_expr_partial = accessor_var_name + ".Create(" + NumToString(value.offset);

      code += " get { return new " + type_name + "(" + create_buf_pos_expr_partial + ")); } }\n\n";

      code += "  public void Get" + method_name + "(out " + return_type + " " + param_name + ") {\n";
      code += "    " + out_value_var_decl + "\n";
      code += "    " + create_buf_pos_expr_partial + ", out " + out_value_var_name + ");\n";
      code += "    " + param_name + " = " + "new " + type_name + "(ref " + out_value_var_name + ");\n";
      code += "  }\n\n";
    } else {
      assert(!struct_def.fixed);

      std::string try_get_field_kind;
      if (type.base_type == BASE_TYPE_VECTOR) {
        try_get_field_kind = "Vector";
      } else {
        assert(type.base_type == BASE_TYPE_STRUCT);

        if (type.struct_def->fixed) {
          try_get_field_kind = "Struct";
        } else {
          try_get_field_kind = "Table";
        }
      }

      std::string try_get_field_value = 
        accessor_var_name + ".TryGet" + try_get_field_kind + "FieldValue(" 
        + NumToString(value.offset) + ", out " + out_value_var_name + ")";
      std::string create_value_expr = "new " + type_name + "(ref " + out_value_var_name + ")";

      code += "\n    get {\n";
      code += "      " + out_value_var_decl + "\n";
      code += "      return " + try_get_field_value;
      code += " ? " + create_value_expr + ": (" + return_type + ")null;\n    }\n  }\n\n";


      std::string try_get_method_name = "TryGet" + method_name;
      code += "  public bool " + try_get_method_name + "(out " + type_name + " " + param_name + ") {\n";
      code += "    " + out_value_var_decl + "\n";
      code += "    if (" + try_get_field_value + ") {\n";
      code += "      " + param_name + " = " + create_value_expr + ";\n";
      code += "      return true;\n";
      code += "    }\n";
      code += "    " + param_name + " = default(" + type_name + ");\n";
      code += "    return false;\n";
      code += "  }\n\n";
    }
  }
}


static void GenStructCsStruct(const LanguageParameters &lang, const Parser &parser,
                              StructDef &struct_def, std::string *code_ptr) {
  if (struct_def.generated) return;
  std::string &code = *code_ptr;

  GenComment(struct_def.doc_comment, code_ptr, &lang.comment_config);

  code += "public ";
  if (struct_def.attributes.Lookup("csharp_partial")) {
    // generate a partial class for this C# struct/table
    code += "partial ";
  }

  std::string csStructName = struct_def.name + "Struct";

  code += "struct " + csStructName;
  //code += lang.inheritance_marker + struct_def.fixed ? "Struct" : "Table";
  code += " {\n";

  std::string accessor_var_type_name;
  std::string accessor_var_name;
  std::string accessor_param_type_name = "BufferPosition";
  std::string accessor_param_name = "bufferPosition";
  std::string accessor_var_assign_rvalue_expr;

  if (struct_def.fixed) {
    accessor_var_type_name = accessor_param_type_name;
    accessor_var_name = "_bufferPosition";
    accessor_var_assign_rvalue_expr = accessor_param_name;
  } else {
    accessor_var_type_name = "TableAccessor";
    accessor_var_name = "_tableAccessor";
    accessor_var_assign_rvalue_expr = "new " + accessor_var_type_name + "(ref " + accessor_param_name + ")";
  }

  code += "  private " + accessor_var_type_name + " " + accessor_var_name + ";\n\n";

  code += "  public " + csStructName + "(" + accessor_param_type_name + " " + accessor_param_name + ") { ";
  code += accessor_var_name + " = " + accessor_var_assign_rvalue_expr + "; }\n";

  code += "  public " + csStructName + "(ref " + accessor_param_type_name + " " + accessor_param_name + ") { ";
  code += accessor_var_name + " = " + accessor_var_assign_rvalue_expr + "; }\n\n";

  if (!struct_def.fixed) {
    // Generate private constructors for use by GetRootAs methods
    std::string byte_buffer_param_name = "buffer";
    std::string byte_buffer_param_decl = "ByteBuffer " + byte_buffer_param_name;
    code += "  private " + csStructName + "(" + byte_buffer_param_decl + ") { ";
    code += accessor_var_type_name + ".CreateFromOffset(" + byte_buffer_param_name + ", out " 
            + accessor_var_name + "); }\n";

    std::string byte_buffer_seg_param_name = "segment";
    std::string byte_buffer_seg_param_decl = "ByteBufferSegment " + byte_buffer_seg_param_name;
    code += "  private " + csStructName + "(ref " + byte_buffer_seg_param_decl + ") { ";
    code += accessor_var_type_name + ".CreateFromOffset(ref " + byte_buffer_seg_param_name + ", out "
      + accessor_var_name + "); }\n\n";

    // Generate a special accessor for the table that when used as the root
    // of a FlatBuffer
    std::string method_name = FunctionStart(lang, 'G') + "etRootAs" + struct_def.name;
    std::string method_signature = "  public static " + csStructName + " " + method_name;
    std::string out_method_signature = "  public static void " + method_name;
    std::string out_param_name = MakeCamel(struct_def.name, false) + "Table";
    std::string out_param_decl = ", out " + csStructName + " " + out_param_name;

    // create method that uses ByteBuffer Position
    code += method_signature + "(" + byte_buffer_param_decl + ") ";
    code += "{ return new " + csStructName + "(" + byte_buffer_param_name+ "); }\n";

    code += out_method_signature + "(" + byte_buffer_param_decl + out_param_decl + ") ";
    code += "{ " + out_param_name + " = new " + csStructName + "(" + byte_buffer_param_name 
            + "); }\n";

    std::string create_from_seg_method_end =
      "new " + csStructName + "(ref " + byte_buffer_seg_param_name + "); }\n"; 
    std::string byte_buffer_seg_ovld_body = "{ return " + create_from_seg_method_end;
    std::string byte_buffer_seg_out_ovld_body = 
      "{ " + out_param_name + " = " + create_from_seg_method_end;

    // create method that uses ByteBufferSegment
    code += method_signature + "(" + byte_buffer_seg_param_decl + ") ";
    code += byte_buffer_seg_ovld_body;

    code += out_method_signature + "(" + byte_buffer_seg_param_decl + out_param_decl + ")";
    code += byte_buffer_seg_out_ovld_body;

    code += method_signature + "(ref " + byte_buffer_seg_param_decl + ") ";
    code += byte_buffer_seg_ovld_body;

    code += out_method_signature + "(ref " + byte_buffer_seg_param_decl + out_param_decl + ") ";
    code += byte_buffer_seg_out_ovld_body;

    code += "\n";

    if (parser.root_struct_def_ == &struct_def) {
      if (parser.file_identifier_.length()) {
        // Check if a buffer has the identifier.
        code += "  public static ";
        code += lang.bool_type + struct_def.name;
        code += "BufferHasIdentifier(ByteBuffer buffer) { return ";
        code += accessor_var_type_name + ".HasIdentifier(buffer, \"" + parser.file_identifier_;
        code += "\"); }\n";
      }
      code += "\n";
    }
  }

  code += "  public " + accessor_var_type_name + " Get" + accessor_var_type_name + "() { ";
  code += "return " + accessor_var_name + "; }\n\n";

  for (auto it = struct_def.fields.vec.begin();
       it != struct_def.fields.vec.end();
       ++it) {
    auto &field = **it;
    if (field.deprecated) continue;
    GenComment(field.doc_comment, code_ptr, &lang.comment_config, "  ");

    if (field.value.type.base_type == BASE_TYPE_UNION) {
      const EnumDef& union_enum_def = *field.value.type.enum_def;
      assert(union_enum_def.is_union);

      std::string accessor_name_root_prefix = MakeCamel(field.name, lang.first_camel_upper) + "As";
      const auto& enum_val_vec = union_enum_def.vals.vec;
      for (auto it = enum_val_vec.begin() + 1; it != enum_val_vec.end(); ++it) {
        std::string accessor_name_root = accessor_name_root_prefix + (*it)->name;
        Type union_member_type(BASE_TYPE_STRUCT, (*it)->struct_def);
        GenFieldAccessors(lang, parser, struct_def, field.value, union_member_type, accessor_name_root, &code);
      }
    } else {
      GenFieldAccessors(lang, parser, struct_def, field.value, field.value.type, field.name, code_ptr);

      // Generate a ByteBuffer accessor for strings
      if (field.value.type.base_type == BASE_TYPE_STRING) {
        std::string field_name_in_camel = MakeCamel(field.name);
        std::string offset_str = NumToString(field.value.offset);

        code += "  public ArraySegment<byte>? Get";
        code += field_name_in_camel;
        code += "Bytes() { return ";
        code += accessor_var_name;
        code += ".GetStringFieldValueAsArraySegment(";
        code += offset_str;
        code += "); }\n";
        code += "  public ByteBufferSegment? Get";
        code += field_name_in_camel;
        code += "BufferSegment() { return ";
        code += accessor_var_name;
        code += ".GetStringFieldValueAsByteBufferSegment(";
        code += offset_str;
        code += "); }\n";
      }

      // generate object accessors if is nested_flatbuffer
      auto nested = field.attributes.Lookup("nested_flatbuffer");
      if (nested) {
        assert(field.value.type.base_type == BASE_TYPE_VECTOR 
               && field.value.type.element == BASE_TYPE_UCHAR);

        auto nested_qualified_name = nested->constant;
        // first attempt lookup using attribute value
        auto nested_type = parser.structs_.Lookup(nested_qualified_name);
        // if not found, try qualifying the name using known namespaces
        if (nested_type == nullptr) {
          for (auto namespaceItr = parser.namespaces_.rbegin();
               namespaceItr != parser.namespaces_.rend() && nested_type == nullptr;
               ++namespaceItr) {
            nested_qualified_name = (*namespaceItr)->GetFullyQualifiedName(nested->constant);
            nested_type = parser.structs_.Lookup(nested_qualified_name);
          }
        }
        // generate helper if we can find the specified flatbuffer
        if (nested_type != nullptr) {
          auto nested_method_name = MakeCamel(field.name, true) + "As" + nested_type->name;
          auto nested_type_name = WrapInNameSpace(parser, *nested_type) + "Struct";
          std::string nested_try_get_method_name = "TryGet" + nested_method_name;
          std::string nested_var_name = MakeCamel(nested_type->name, false) + "Table";
          std::string nested_var_decl_expr = nested_type_name + " " + nested_var_name;
          std::string vector_pos_var_name = "vectorPosition";
          std::string vector_pos_var_decl = "BufferPosition " + vector_pos_var_name + ";";
          std::string try_get_vector_pos_expr =  
            accessor_var_name + ".TryGetVectorFieldValue(" + NumToString(field.value.offset)
            + ", out " + vector_pos_var_name + ")";
          std::string nested_table_pos_var_name = "tablePosition";
          std::string nested_table_pos_var_decl = "BufferPosition " + nested_table_pos_var_name + ";";
          std::string get_nested_table_pos_stmt =
            "VectorAccessor.GetNestedFlatBufferTable(ref " + vector_pos_var_name + ", out "
            + nested_table_pos_var_name + ");";
          std::string create_nested_type_expr = 
            "new " + nested_type_name + "(ref " + nested_table_pos_var_name + ")";
          std::string return_type = nested_type_name + "?";

          code += "  public " + return_type + " " + nested_method_name + " {\n";
          code += "    get {\n      " + nested_var_decl_expr + ";\n";
          code += "      return " + nested_try_get_method_name + "(out " + nested_var_name + ")";
          code += " ? " + nested_var_name + " : (" + return_type + ")null;\n    }\n  }\n\n";
          code += "  public bool " + nested_try_get_method_name + "(out " + nested_var_decl_expr + ") {\n";
          code += "    " + vector_pos_var_decl + "\n";
          code += "    if (" + try_get_vector_pos_expr + ") {\n";
          code += "      " + nested_table_pos_var_decl + "\n";
          code += "      " + get_nested_table_pos_stmt + "\n";
          code += "      " + nested_var_name + " = " + create_nested_type_expr + ";\n";
          code += "      return true;\n    }\n";
          code += "    " + nested_var_name + " = default(" + nested_type_name + ");\n";
          code += "    return false;\n  }\n\n";
        }
      }
    }
  }
  code += "\n";
  code += "}\n\n";
}

static void GenVectorCsStruct(const LanguageParameters &lang, const Parser &parser,
                              const Type& type, std::string *code_ptr) {
  assert(IsEnum(type) || type.base_type == BASE_TYPE_STRUCT);

  std::string& code = *code_ptr;
  if (IsEnum(type) && type.enum_def->generated
      || type.base_type == BASE_TYPE_STRUCT && type.struct_def->generated) {
    return;
  }

  std::string type_name = GenTypeGet(lang, parser, type);
  std::string vector_struct_type_name = MakeCamel(type_name) + "Vector";
  std::string vector_accessor_type_name = "VectorAccessor";
  std::string vector_accessor_var_name = "_vectorAccessor";
  std::string cons_param_type_name = "BufferPosition";
  std::string cons_param_var_name = "vectorPosition";
  std::string cons_param_var_decl_expr = cons_param_type_name + " " + cons_param_var_name;
  std::string cons_body = "{ " + vector_accessor_var_name + " = new " + vector_accessor_type_name 
                          + "(ref " + cons_param_var_name + "); }";
  std::string return_type_name = GenTypeGetCsStruct(lang, parser, type);

  code += "public struct " + vector_struct_type_name + " : ";
  if (IsEnum(type)) {
    code += "IVector";
  } else {
    code += "IFieldGroupVector";
  }
  code += "<" + return_type_name + ">";
  code += " {\n";
  code += "  private " + vector_accessor_type_name + " " + vector_accessor_var_name + ";\n\n";
  code += "  public " + vector_struct_type_name + "(" + cons_param_var_decl_expr +") ";
  code += cons_body + "\n";
  code += "  public " + vector_struct_type_name + "(ref " + cons_param_var_decl_expr + ") ";
  code += cons_body + "\n\n";
  code += "  public " + vector_accessor_type_name + " " + vector_accessor_type_name + " { ";
  code += "get { return " + vector_accessor_var_name + "; } }\n";
  code += "  public int Length { get { return " + vector_accessor_var_name
          + ".VectorDataLength; } }\n";
  code += "  public ArraySegment<byte> GetAsArraySegment() { return " + vector_accessor_var_name
          + ".GetVectorAsArraySegment(); }\n";
  code += "  public void GetAsArraySegment(out ArraySegment<byte> arraySegment) { " 
          + vector_accessor_var_name + ".GetVectorAsArraySegment(out arraySegment); }\n";
  code += "  public ByteBufferSegment GetAsByteBufferSegment() { return "
          + vector_accessor_var_name + ".GetVectorAsByteBufferSegment(); }\n";
  code += "  public void GetAsByteBufferSegment(out ByteBufferSegment byteBufferSegment) { ";
  code += vector_accessor_var_name + ".GetVectorAsByteBufferSegment(";
  code += "out byteBufferSegment); }\n";
  
  std::string enumerator_type_name;
  if (type.base_type == BASE_TYPE_STRUCT)
    enumerator_type_name = "FieldGroup";
  enumerator_type_name += "VectorEnumerator" "<" + return_type_name + ", "
    + vector_struct_type_name + ">";

  code += "  public " + enumerator_type_name + " GetEnumerator() { ";
  code += "return new " + enumerator_type_name + "(ref this); }\n";
  code += "  System.Collections.Generic.IEnumerator<" + return_type_name + "> ";
  code += "System.Collections.Generic.IEnumerable<" + return_type_name + ">.GetEnumerator() { ";
  code += "return GetEnumerator(); }\n";
  code += "  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { ";
  code += "return GetEnumerator(); }\n\n";

  std::string index_var_name = "index";

  std::string get_item_body;
  std::string get_item_return_expr;
  std::string set_item_expr;
  if (IsEnum(type)) {
    std::string item_method_suffix = MakeCamel(GenTypeBasic(lang, parser, type, false)) + "Item";
    std::string get_item_method = "Get" + item_method_suffix;
    std::string get_cast = DestinationCast(lang, parser, type);
    get_item_return_expr = get_cast + vector_accessor_var_name + "." + get_item_method + "("
                           + index_var_name + ")";

    std::string put_item_method = "Put" + item_method_suffix;
    std::string put_cast = SourceCast(lang, parser, type);
    set_item_expr = vector_accessor_var_name + "." + put_item_method + "(" + index_var_name
                    + ", " + put_cast + "value)";
  } else {
    std::string item_pos_var_name = "itemPosition";
    std::string item_pos_var_decl = "BufferPosition " + item_pos_var_name + ";";
    std::string item_var_name = "item";
    std::string item_var_decl_expr = return_type_name + " " + item_var_name;
    std::string vector_accessor_get_item = "Get";
    vector_accessor_get_item += (type.struct_def->fixed ? "Struct" : "Table");
    vector_accessor_get_item += "Item";

    code += "  public void GetItem(int " + index_var_name + ", out " + item_var_decl_expr + ") {\n";
    code += "    " + item_pos_var_decl + "\n";
    code += "    " + vector_accessor_var_name + ".";
    code += vector_accessor_get_item;
    code += "(" + index_var_name;
    if (type.struct_def->fixed)
      code += ", " + NumToString(InlineSize(type));
    code += ", out " + item_pos_var_name + ");\n";
    code += "    " + item_var_name + " = new " + return_type_name + "(ref " + item_pos_var_name + ");\n";
    code += "  }\n\n";

    get_item_body += "      " + item_var_decl_expr + ";\n";
    get_item_body += "      GetItem(" + index_var_name + ", out " + item_var_name + ");";
    get_item_return_expr = item_var_name;
    set_item_expr = "throw new NotSupportedException()";
  }

  code += "  public " + return_type_name + " this[int " + index_var_name + "] {\n";
  code += "    get { ";
  if (!get_item_body.empty())
    code += "\n" + get_item_body + "\n      ";
  code += "return " + get_item_return_expr + ";";
  if (!get_item_body.empty())
    code += "\n   ";
  code += " }\n";
  code += "    set { " + set_item_expr + "; }\n  }\n";
  code += "}\n\n";
}

// Save out the generated code for a single class while adding
// declaration boilerplate.
static bool SaveClass(const LanguageParameters &lang, const Parser &parser,
                      const std::string &defname, const std::string &classcode,
                      const std::string &path, bool needs_includes, bool onefile) {
  if (!classcode.length()) return true;

  std::string namespace_general;
  std::string namespace_dir = path;  // Either empty or ends in separator.
  auto &namespaces = parser.namespaces_.back()->components;
  for (auto it = namespaces.begin(); it != namespaces.end(); ++it) {
    if (namespace_general.length()) {
      namespace_general += ".";
    }
    namespace_general += *it;
    if (!onefile) {
      namespace_dir += *it + kPathSeparator;
    }

  }
  EnsureDirExists(namespace_dir);

  std::string code = "// automatically generated, do not modify\n\n";
  if (!namespace_general.empty()) {
    code += lang.namespace_ident + namespace_general + lang.namespace_begin;
    code += "\n\n";
  }
  if (needs_includes) code += lang.includes;
  code += classcode;
  if (!namespace_general.empty()) code += lang.namespace_end;
  auto filename = namespace_dir + defname + lang.file_extension;
  return SaveFile(filename.c_str(), code, false);
}

bool GenerateGeneral(const Parser &parser,
                     const std::string &path,
                     const std::string & file_name) {

  assert(parser.opts.lang <= IDLOptions::kMAX);
  auto lang = language_parameters[parser.opts.lang];
  std::string one_file_code;

  for (auto it = parser.enums_.vec.begin();
       it != parser.enums_.vec.end(); ++it) {
    std::string enumcode;
    GenEnum(lang, parser, **it, &enumcode);
    if (parser.opts.one_file) {
      one_file_code += enumcode;
    }
    else {
      if (!SaveClass(lang, parser, (**it).name, enumcode, path, false, false))
        return false;
    }
  }

  for (auto it = parser.structs_.vec.begin();
       it != parser.structs_.vec.end(); ++it) {
    std::string declcode;
    GenStruct(lang, parser, **it, &declcode);
    if (parser.opts.one_file) {
      one_file_code += declcode;
    }
    else {
      if (!SaveClass(lang, parser, (**it).name, declcode, path, true, false))
        return false;
    }
  }

  if (lang.language == IDLOptions::kCSharp && parser.opts.generate_cs_structs) {
    for (auto it = parser.structs_.vec.begin();
         it != parser.structs_.vec.end(); ++it) {
      std::string declcode;
      GenStructCsStruct(lang, parser, **it, &declcode);
      if (parser.opts.one_file) {
        one_file_code += declcode;
      } else {
        if (!SaveClass(lang, parser, (**it).name + "Struct", declcode, path, true, false))
          return false;
      }
    }

    for (auto it = parser.enums_.vec.begin();
         it != parser.enums_.vec.end(); ++it) {
      std::string declcode;
      GenVectorCsStruct(lang, parser, (**it).underlying_type, &declcode);
      if (parser.opts.one_file) {
        one_file_code += declcode;
      } else {
        if (!SaveClass(lang, parser, (**it).name + "Vector", declcode, path, true, false))
          return false;
      }
    }

    for (auto it = parser.structs_.vec.begin();
         it != parser.structs_.vec.end(); ++it) {
      std::string declcode;
      GenVectorCsStruct(lang, parser, Type(BASE_TYPE_STRUCT, *it), &declcode);
      if (parser.opts.one_file) {
        one_file_code += declcode;
      } else {
        if (!SaveClass(lang, parser, (**it).name + "Vector", declcode, path, true, false))
          return false;
      }
    }
  }

  if (parser.opts.one_file) {
    return SaveClass(lang, parser, file_name, one_file_code,path, true, true);
  }
  return true;
}

static std::string ClassFileName(const LanguageParameters &lang,
                                 const Parser &parser, const Definition &def,
                                 const std::string &path) {
  std::string namespace_general;
  std::string namespace_dir = path;
  auto &namespaces = parser.namespaces_.back()->components;
  for (auto it = namespaces.begin(); it != namespaces.end(); ++it) {
    if (namespace_general.length()) {
      namespace_general += ".";
      namespace_dir += kPathSeparator;
    }
    namespace_general += *it;
    namespace_dir += *it;
  }

  return namespace_dir + kPathSeparator + def.name + lang.file_extension;
}

std::string GeneralMakeRule(const Parser &parser,
                            const std::string &path,
                            const std::string &file_name) {
  assert(parser.opts.lang <= IDLOptions::kMAX);
  auto lang = language_parameters[parser.opts.lang];

  std::string make_rule;

  for (auto it = parser.enums_.vec.begin();
       it != parser.enums_.vec.end(); ++it) {
    if (make_rule != "")
      make_rule += " ";
    make_rule += ClassFileName(lang, parser, **it, path);
  }

  for (auto it = parser.structs_.vec.begin();
       it != parser.structs_.vec.end(); ++it) {
    if (make_rule != "")
      make_rule += " ";
    make_rule += ClassFileName(lang, parser, **it, path);
  }

  make_rule += ": ";
  auto included_files = parser.GetIncludedFilesRecursive(file_name);
  for (auto it = included_files.begin();
       it != included_files.end(); ++it) {
    make_rule += " " + *it;
  }
  return make_rule;
}

std::string BinaryFileName(const Parser &parser,
                           const std::string &path,
                           const std::string &file_name) {
  auto ext = parser.file_extension_.length() ? parser.file_extension_ : "bin";
  return path + file_name + "." + ext;
}

bool GenerateBinary(const Parser &parser,
                    const std::string &path,
                    const std::string &file_name) {
  return !parser.builder_.GetSize() ||
         flatbuffers::SaveFile(
           BinaryFileName(parser, path, file_name).c_str(),
           reinterpret_cast<char *>(parser.builder_.GetBufferPointer()),
           parser.builder_.GetSize(),
           true);
}

std::string BinaryMakeRule(const Parser &parser,
                           const std::string &path,
                           const std::string &file_name) {
  if (!parser.builder_.GetSize()) return "";
  std::string filebase = flatbuffers::StripPath(
      flatbuffers::StripExtension(file_name));
  std::string make_rule = BinaryFileName(parser, path, filebase) + ": " +
      file_name;
  auto included_files = parser.GetIncludedFilesRecursive(
      parser.root_struct_def_->file);
  for (auto it = included_files.begin();
       it != included_files.end(); ++it) {
    make_rule += " " + *it;
  }
  return make_rule;
}

}  // namespace flatbuffers
