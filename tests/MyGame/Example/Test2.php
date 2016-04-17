<?php
// automatically generated, do not modify

namespace MyGame\Example;

use \Google\FlatBuffers\Struct;
use \Google\FlatBuffers\Table;
use \Google\FlatBuffers\ByteBuffer;
use \Google\FlatBuffers\FlatBufferBuilder;

class Test2 extends Struct
{
    /**
     * @param int $_i offset
     * @param ByteBuffer $_bb
     * @return Test2
     **/
    public function init($_i, ByteBuffer $_bb)
    {
        $this->bb_pos = $_i;
        $this->bb = $_bb;
        return $this;
    }

    /**
     * @return sbyte
     */
    public function GetB()
    {
        return $this->bb->getSbyte($this->bb_pos + 0);
    }


    /**
     * @return int offset
     */
    public static function createTest2(FlatBufferBuilder $builder, $b)
    {
        $builder->prep(1, 1);
        $builder->putSbyte($b);
        return $builder->offset();
    }
}
