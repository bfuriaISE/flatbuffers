// automatically generated, do not modify

package Example

import (
	flatbuffers "github.com/google/flatbuffers/go"
)
type Test2 struct {
	_tab flatbuffers.Struct
}

func (rcv *Test2) Init(buf []byte, i flatbuffers.UOffsetT) {
	rcv._tab.Bytes = buf
	rcv._tab.Pos = i
}

func (rcv *Test2) B() int8 { return rcv._tab.GetInt8(rcv._tab.Pos + flatbuffers.UOffsetT(0)) }

func CreateTest2(builder *flatbuffers.Builder, b int8) flatbuffers.UOffsetT {
    builder.Prep(1, 1)
    builder.PrependInt8(b)
    return builder.Offset()
}
