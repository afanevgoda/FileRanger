import React, { useState } from "react";
import { Divider, Radio } from 'antd';

export default function SnapshotSelector({ snapshots, setSelectedSnapshot }) {
    const onChange = ({ target: { value } }) => setSelectedSnapshot(value);

    let comp;
    if (snapshots.length == 0) {
        comp = <span>No snapshots found</span>
    } else {
        comp = <>
            <Radio.Group onChange={onChange}>
                {snapshots.map(x => (
                    <Radio.Button value={x}>{x.createdAt}</Radio.Button>
                ))}
            </Radio.Group>
        </>
    }

    return (
        <>
            <Divider type="vertical"></Divider>
            {comp}
        </>
    )
}