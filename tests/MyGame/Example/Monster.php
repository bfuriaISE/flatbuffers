<?php
// automatically generated, do not modify

namespace MyGame\Example;

use \Google\FlatBuffers\Struct;
use \Google\FlatBuffers\Table;
use \Google\FlatBuffers\ByteBuffer;
use \Google\FlatBuffers\FlatBufferBuilder;

/// an example documentation comment: monster object
class Monster extends Table
{
    /**
     * @param ByteBuffer $bb
     * @return Monster
     */
    public static function getRootAsMonster(ByteBuffer $bb)
    {
        $obj = new Monster();
        return ($obj->init($bb->getInt($bb->getPosition()) + $bb->getPosition(), $bb));
    }

    public static function MonsterIdentifier()
    {
        return "MONS";
    }

    public static function MonsterBufferHasIdentifier(ByteBuffer $buf)
    {
        return self::__has_identifier($buf, self::MonsterIdentifier());
    }

    public static function MonsterExtension()
    {
        return "mon";
    }

    /**
     * @param int $_i offset
     * @param ByteBuffer $_bb
     * @return Monster
     **/
    public function init($_i, ByteBuffer $_bb)
    {
        $this->bb_pos = $_i;
        $this->bb = $_bb;
        return $this;
    }

    public function getPos()
    {
        $obj = new Vec3();
        $o = $this->__offset(4);
        return $o != 0 ? $obj->init($o + $this->bb_pos, $this->bb) : 0;
    }

    /**
     * @return short
     */
    public function getMana()
    {
        $o = $this->__offset(6);
        return $o != 0 ? $this->bb->getShort($o + $this->bb_pos) : 150;
    }

    /**
     * @return short
     */
    public function getHp()
    {
        $o = $this->__offset(8);
        return $o != 0 ? $this->bb->getShort($o + $this->bb_pos) : 100;
    }

    public function getName()
    {
        $o = $this->__offset(10);
        return $o != 0 ? $this->__string($o + $this->bb_pos) : null;
    }

    /**
     * @param int offset
     * @return byte
     */
    public function getInventory($j)
    {
        $o = $this->__offset(14);
        return $o != 0 ? $this->bb->getByte($this->__vector($o) + $j * 1) : 0;
    }

    /**
     * @return int
     */
    public function getInventoryLength()
    {
        $o = $this->__offset(14);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return sbyte
     */
    public function getColor()
    {
        $o = $this->__offset(16);
        return $o != 0 ? $this->bb->getSbyte($o + $this->bb_pos) : \MyGame\Example\Color::Blue;
    }

    /**
     * @return byte
     */
    public function getTestType()
    {
        $o = $this->__offset(18);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : \MyGame\Example\Any::NONE;
    }

    /**
     * @returnint
     */
    public function getTest($obj)
    {
        $o = $this->__offset(20);
        return $o != 0 ? $this->__union($obj, $o) : null;
    }

    /**
     * @returnVectorOffset
     */
    public function getTest4($j)
    {
        $o = $this->__offset(22);
        $obj = new Test();
        return $o != 0 ? $obj->init($this->__vector($o) + $j *4, $this->bb) : null;
    }

    /**
     * @return int
     */
    public function getTest4Length()
    {
        $o = $this->__offset(22);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @param int offset
     * @return string
     */
    public function getTestarrayofstring($j)
    {
        $o = $this->__offset(24);
        return $o != 0 ? $this->__string($this->__vector($o) + $j * 4) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofstringLength()
    {
        $o = $this->__offset(24);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

/// an example documentation comment: this will end up in the generated code
/// multiline too
    /**
     * @returnVectorOffset
     */
    public function getTestarrayoftables($j)
    {
        $o = $this->__offset(26);
        $obj = new Monster();
        return $o != 0 ? $obj->init($this->__indirect($this->__vector($o) + $j * 4), $this->bb) : null;
    }

    /**
     * @return int
     */
    public function getTestarrayoftablesLength()
    {
        $o = $this->__offset(26);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    public function getEnemy()
    {
        $obj = new Monster();
        $o = $this->__offset(28);
        return $o != 0 ? $obj->init($this->__indirect($o + $this->bb_pos), $this->bb) : 0;
    }

    /**
     * @param int offset
     * @return byte
     */
    public function getTestnestedflatbuffer($j)
    {
        $o = $this->__offset(30);
        return $o != 0 ? $this->bb->getByte($this->__vector($o) + $j * 1) : 0;
    }

    /**
     * @return int
     */
    public function getTestnestedflatbufferLength()
    {
        $o = $this->__offset(30);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    public function getTestempty()
    {
        $obj = new Stat();
        $o = $this->__offset(32);
        return $o != 0 ? $obj->init($this->__indirect($o + $this->bb_pos), $this->bb) : 0;
    }

    /**
     * @return bool
     */
    public function getTestbool()
    {
        $o = $this->__offset(34);
        return $o != 0 ? $this->bb->getBool($o + $this->bb_pos) : false;
    }

    /**
     * @return int
     */
    public function getTesthashs32Fnv1()
    {
        $o = $this->__offset(36);
        return $o != 0 ? $this->bb->getInt($o + $this->bb_pos) : 0;
    }

    /**
     * @return uint
     */
    public function getTesthashu32Fnv1()
    {
        $o = $this->__offset(38);
        return $o != 0 ? $this->bb->getUint($o + $this->bb_pos) : 0;
    }

    /**
     * @return long
     */
    public function getTesthashs64Fnv1()
    {
        $o = $this->__offset(40);
        return $o != 0 ? $this->bb->getLong($o + $this->bb_pos) : 0;
    }

    /**
     * @return ulong
     */
    public function getTesthashu64Fnv1()
    {
        $o = $this->__offset(42);
        return $o != 0 ? $this->bb->getUlong($o + $this->bb_pos) : 0;
    }

    /**
     * @return int
     */
    public function getTesthashs32Fnv1a()
    {
        $o = $this->__offset(44);
        return $o != 0 ? $this->bb->getInt($o + $this->bb_pos) : 0;
    }

    /**
     * @return uint
     */
    public function getTesthashu32Fnv1a()
    {
        $o = $this->__offset(46);
        return $o != 0 ? $this->bb->getUint($o + $this->bb_pos) : 0;
    }

    /**
     * @return long
     */
    public function getTesthashs64Fnv1a()
    {
        $o = $this->__offset(48);
        return $o != 0 ? $this->bb->getLong($o + $this->bb_pos) : 0;
    }

    /**
     * @return ulong
     */
    public function getTesthashu64Fnv1a()
    {
        $o = $this->__offset(50);
        return $o != 0 ? $this->bb->getUlong($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return bool
     */
    public function getTestarrayofbools($j)
    {
        $o = $this->__offset(52);
        return $o != 0 ? $this->bb->getBool($this->__vector($o) + $j * 1) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofboolsLength()
    {
        $o = $this->__offset(52);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @param int offset
     * @return byte
     */
    public function getTestarrayofbytes($j)
    {
        $o = $this->__offset(54);
        return $o != 0 ? $this->bb->getByte($this->__vector($o) + $j * 1) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofbytesLength()
    {
        $o = $this->__offset(54);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte2()
    {
        $o = $this->__offset(56);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return bool
     */
    public function getTestarrayofbools1($j)
    {
        $o = $this->__offset(58);
        return $o != 0 ? $this->bb->getBool($this->__vector($o) + $j * 1) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofbools1Length()
    {
        $o = $this->__offset(58);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte3()
    {
        $o = $this->__offset(60);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return short
     */
    public function getTestarrayofshorts($j)
    {
        $o = $this->__offset(62);
        return $o != 0 ? $this->bb->getShort($this->__vector($o) + $j * 2) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofshortsLength()
    {
        $o = $this->__offset(62);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte4()
    {
        $o = $this->__offset(64);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return int
     */
    public function getTestarrayofints($j)
    {
        $o = $this->__offset(66);
        return $o != 0 ? $this->bb->getInt($this->__vector($o) + $j * 4) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofintsLength()
    {
        $o = $this->__offset(66);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte5()
    {
        $o = $this->__offset(68);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return long
     */
    public function getTestarrayoflongs($j)
    {
        $o = $this->__offset(70);
        return $o != 0 ? $this->bb->getLong($this->__vector($o) + $j * 8) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayoflongsLength()
    {
        $o = $this->__offset(70);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte6()
    {
        $o = $this->__offset(72);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return float
     */
    public function getTestarrayoffloats($j)
    {
        $o = $this->__offset(74);
        return $o != 0 ? $this->bb->getFloat($this->__vector($o) + $j * 4) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayoffloatsLength()
    {
        $o = $this->__offset(74);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte7()
    {
        $o = $this->__offset(76);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param int offset
     * @return double
     */
    public function getTestarrayofdoubles($j)
    {
        $o = $this->__offset(78);
        return $o != 0 ? $this->bb->getDouble($this->__vector($o) + $j * 8) : 0;
    }

    /**
     * @return int
     */
    public function getTestarrayofdoublesLength()
    {
        $o = $this->__offset(78);
        return $o != 0 ? $this->__vector_len($o) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte8()
    {
        $o = $this->__offset(80);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @return byte
     */
    public function getTestbyte1()
    {
        $o = $this->__offset(82);
        return $o != 0 ? $this->bb->getByte($o + $this->bb_pos) : 0;
    }

    /**
     * @param FlatBufferBuilder $builder
     * @return void
     */
    public static function startMonster(FlatBufferBuilder $builder)
    {
        $builder->StartObject(40);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @return Monster
     */
    public static function createMonster(FlatBufferBuilder $builder, $pos, $mana, $hp, $name, $inventory, $color, $test_type, $test, $test4, $testarrayofstring, $testarrayoftables, $enemy, $testnestedflatbuffer, $testempty, $testbool, $testhashs32_fnv1, $testhashu32_fnv1, $testhashs64_fnv1, $testhashu64_fnv1, $testhashs32_fnv1a, $testhashu32_fnv1a, $testhashs64_fnv1a, $testhashu64_fnv1a, $testarrayofbools, $testarrayofbytes, $testbyte2, $testarrayofbools1, $testbyte3, $testarrayofshorts, $testbyte4, $testarrayofints, $testbyte5, $testarrayoflongs, $testbyte6, $testarrayoffloats, $testbyte7, $testarrayofdoubles, $testbyte8, $testbyte1)
    {
        $builder->startObject(40);
        self::addPos($builder, $pos);
        self::addMana($builder, $mana);
        self::addHp($builder, $hp);
        self::addName($builder, $name);
        self::addInventory($builder, $inventory);
        self::addColor($builder, $color);
        self::addTestType($builder, $test_type);
        self::addTest($builder, $test);
        self::addTest4($builder, $test4);
        self::addTestarrayofstring($builder, $testarrayofstring);
        self::addTestarrayoftables($builder, $testarrayoftables);
        self::addEnemy($builder, $enemy);
        self::addTestnestedflatbuffer($builder, $testnestedflatbuffer);
        self::addTestempty($builder, $testempty);
        self::addTestbool($builder, $testbool);
        self::addTesthashs32Fnv1($builder, $testhashs32_fnv1);
        self::addTesthashu32Fnv1($builder, $testhashu32_fnv1);
        self::addTesthashs64Fnv1($builder, $testhashs64_fnv1);
        self::addTesthashu64Fnv1($builder, $testhashu64_fnv1);
        self::addTesthashs32Fnv1a($builder, $testhashs32_fnv1a);
        self::addTesthashu32Fnv1a($builder, $testhashu32_fnv1a);
        self::addTesthashs64Fnv1a($builder, $testhashs64_fnv1a);
        self::addTesthashu64Fnv1a($builder, $testhashu64_fnv1a);
        self::addTestarrayofbools($builder, $testarrayofbools);
        self::addTestarrayofbytes($builder, $testarrayofbytes);
        self::addTestbyte2($builder, $testbyte2);
        self::addTestarrayofbools1($builder, $testarrayofbools1);
        self::addTestbyte3($builder, $testbyte3);
        self::addTestarrayofshorts($builder, $testarrayofshorts);
        self::addTestbyte4($builder, $testbyte4);
        self::addTestarrayofints($builder, $testarrayofints);
        self::addTestbyte5($builder, $testbyte5);
        self::addTestarrayoflongs($builder, $testarrayoflongs);
        self::addTestbyte6($builder, $testbyte6);
        self::addTestarrayoffloats($builder, $testarrayoffloats);
        self::addTestbyte7($builder, $testbyte7);
        self::addTestarrayofdoubles($builder, $testarrayofdoubles);
        self::addTestbyte8($builder, $testbyte8);
        self::addTestbyte1($builder, $testbyte1);
        $o = $builder->endObject();
        $builder->required($o, 10);  // name
        return $o;
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int
     * @return void
     */
    public static function addPos(FlatBufferBuilder $builder, $pos)
    {
        $builder->addStructX(0, $pos, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param short
     * @return void
     */
    public static function addMana(FlatBufferBuilder $builder, $mana)
    {
        $builder->addShortX(1, $mana, 150);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param short
     * @return void
     */
    public static function addHp(FlatBufferBuilder $builder, $hp)
    {
        $builder->addShortX(2, $hp, 100);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param StringOffset
     * @return void
     */
    public static function addName(FlatBufferBuilder $builder, $name)
    {
        $builder->addOffsetX(3, $name, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addInventory(FlatBufferBuilder $builder, $inventory)
    {
        $builder->addOffsetX(5, $inventory, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createInventoryVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(1, count($data), 1);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addByte($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startInventoryVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(1, $numElems, 1);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param sbyte
     * @return void
     */
    public static function addColor(FlatBufferBuilder $builder, $color)
    {
        $builder->addSbyteX(6, $color, 8);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestType(FlatBufferBuilder $builder, $testType)
    {
        $builder->addByteX(7, $testType, 0);
    }

    public static function addTest(FlatBufferBuilder $builder, $offset)
    {
        $builder->addOffsetX(8, $offset, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTest4(FlatBufferBuilder $builder, $test4)
    {
        $builder->addOffsetX(9, $test4, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTest4Vector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(4, count($data), 2);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addOffset($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTest4Vector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(4, $numElems, 2);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofstring(FlatBufferBuilder $builder, $testarrayofstring)
    {
        $builder->addOffsetX(10, $testarrayofstring, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofstringVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(4, count($data), 4);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addOffset($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofstringVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(4, $numElems, 4);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayoftables(FlatBufferBuilder $builder, $testarrayoftables)
    {
        $builder->addOffsetX(11, $testarrayoftables, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayoftablesVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(4, count($data), 4);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addOffset($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayoftablesVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(4, $numElems, 4);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int
     * @return void
     */
    public static function addEnemy(FlatBufferBuilder $builder, $enemy)
    {
        $builder->addOffsetX(12, $enemy, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestnestedflatbuffer(FlatBufferBuilder $builder, $testnestedflatbuffer)
    {
        $builder->addOffsetX(13, $testnestedflatbuffer, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestnestedflatbufferVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(1, count($data), 1);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addByte($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestnestedflatbufferVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(1, $numElems, 1);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int
     * @return void
     */
    public static function addTestempty(FlatBufferBuilder $builder, $testempty)
    {
        $builder->addOffsetX(14, $testempty, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param bool
     * @return void
     */
    public static function addTestbool(FlatBufferBuilder $builder, $testbool)
    {
        $builder->addBoolX(15, $testbool, false);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int
     * @return void
     */
    public static function addTesthashs32Fnv1(FlatBufferBuilder $builder, $testhashs32Fnv1)
    {
        $builder->addIntX(16, $testhashs32Fnv1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param uint
     * @return void
     */
    public static function addTesthashu32Fnv1(FlatBufferBuilder $builder, $testhashu32Fnv1)
    {
        $builder->addUintX(17, $testhashu32Fnv1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param long
     * @return void
     */
    public static function addTesthashs64Fnv1(FlatBufferBuilder $builder, $testhashs64Fnv1)
    {
        $builder->addLongX(18, $testhashs64Fnv1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param ulong
     * @return void
     */
    public static function addTesthashu64Fnv1(FlatBufferBuilder $builder, $testhashu64Fnv1)
    {
        $builder->addUlongX(19, $testhashu64Fnv1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int
     * @return void
     */
    public static function addTesthashs32Fnv1a(FlatBufferBuilder $builder, $testhashs32Fnv1a)
    {
        $builder->addIntX(20, $testhashs32Fnv1a, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param uint
     * @return void
     */
    public static function addTesthashu32Fnv1a(FlatBufferBuilder $builder, $testhashu32Fnv1a)
    {
        $builder->addUintX(21, $testhashu32Fnv1a, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param long
     * @return void
     */
    public static function addTesthashs64Fnv1a(FlatBufferBuilder $builder, $testhashs64Fnv1a)
    {
        $builder->addLongX(22, $testhashs64Fnv1a, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param ulong
     * @return void
     */
    public static function addTesthashu64Fnv1a(FlatBufferBuilder $builder, $testhashu64Fnv1a)
    {
        $builder->addUlongX(23, $testhashu64Fnv1a, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofbools(FlatBufferBuilder $builder, $testarrayofbools)
    {
        $builder->addOffsetX(24, $testarrayofbools, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofboolsVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(1, count($data), 1);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addBool($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofboolsVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(1, $numElems, 1);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofbytes(FlatBufferBuilder $builder, $testarrayofbytes)
    {
        $builder->addOffsetX(25, $testarrayofbytes, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofbytesVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(1, count($data), 1);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addByte($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofbytesVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(1, $numElems, 1);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte2(FlatBufferBuilder $builder, $testbyte2)
    {
        $builder->addByteX(26, $testbyte2, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofbools1(FlatBufferBuilder $builder, $testarrayofbools1)
    {
        $builder->addOffsetX(27, $testarrayofbools1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofbools1Vector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(1, count($data), 1);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addBool($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofbools1Vector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(1, $numElems, 1);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte3(FlatBufferBuilder $builder, $testbyte3)
    {
        $builder->addByteX(28, $testbyte3, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofshorts(FlatBufferBuilder $builder, $testarrayofshorts)
    {
        $builder->addOffsetX(29, $testarrayofshorts, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofshortsVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(2, count($data), 2);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addShort($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofshortsVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(2, $numElems, 2);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte4(FlatBufferBuilder $builder, $testbyte4)
    {
        $builder->addByteX(30, $testbyte4, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofints(FlatBufferBuilder $builder, $testarrayofints)
    {
        $builder->addOffsetX(31, $testarrayofints, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofintsVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(4, count($data), 4);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addInt($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofintsVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(4, $numElems, 4);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte5(FlatBufferBuilder $builder, $testbyte5)
    {
        $builder->addByteX(32, $testbyte5, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayoflongs(FlatBufferBuilder $builder, $testarrayoflongs)
    {
        $builder->addOffsetX(33, $testarrayoflongs, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayoflongsVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(8, count($data), 8);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addLong($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayoflongsVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(8, $numElems, 8);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte6(FlatBufferBuilder $builder, $testbyte6)
    {
        $builder->addByteX(34, $testbyte6, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayoffloats(FlatBufferBuilder $builder, $testarrayoffloats)
    {
        $builder->addOffsetX(35, $testarrayoffloats, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayoffloatsVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(4, count($data), 4);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addFloat($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayoffloatsVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(4, $numElems, 4);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte7(FlatBufferBuilder $builder, $testbyte7)
    {
        $builder->addByteX(36, $testbyte7, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param VectorOffset
     * @return void
     */
    public static function addTestarrayofdoubles(FlatBufferBuilder $builder, $testarrayofdoubles)
    {
        $builder->addOffsetX(37, $testarrayofdoubles, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param array offset array
     * @return int vector offset
     */
    public static function createTestarrayofdoublesVector(FlatBufferBuilder $builder, array $data)
    {
        $builder->startVector(8, count($data), 8);
        for ($i = count($data) - 1; $i >= 0; $i--) {
            $builder->addDouble($data[$i]);
        }
        return $builder->endVector();
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param int $numElems
     * @return void
     */
    public static function startTestarrayofdoublesVector(FlatBufferBuilder $builder, $numElems)
    {
        $builder->startVector(8, $numElems, 8);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte8(FlatBufferBuilder $builder, $testbyte8)
    {
        $builder->addByteX(38, $testbyte8, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @param byte
     * @return void
     */
    public static function addTestbyte1(FlatBufferBuilder $builder, $testbyte1)
    {
        $builder->addByteX(39, $testbyte1, 0);
    }

    /**
     * @param FlatBufferBuilder $builder
     * @return int table offset
     */
    public static function endMonster(FlatBufferBuilder $builder)
    {
        $o = $builder->endObject();
        $builder->required($o, 10);  // name
        return $o;
    }

    public static function finishMonsterBuffer(FlatBufferBuilder $builder, $offset)
    {
        $builder->finish($offset, "MONS");
    }
}
