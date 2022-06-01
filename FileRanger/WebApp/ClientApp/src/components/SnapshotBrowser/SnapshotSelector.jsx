import React, { useState } from "react";
import { Divider, Radio } from 'antd';
import { FastBackwardOutlined } from '@ant-design/icons';
import { defaultFormat } from '../../helpers/dateHelper';
import styles from '../../components/SnapshotBrowser/SnapshotSelector.module.css';

export default function SnapshotSelector({ snapshots, setSelectedSnapshot }) {
    const onChange = ({ target: { value } }) => setSelectedSnapshot(value);

    let comp;
    if (snapshots.length == 0) {
        comp = <span>No snapshots found</span>
    } else {
        comp = <>
            <Radio.Group onChange={onChange}>
                {snapshots.map(x => (
                    <Radio.Button
                        icon={<FastBackwardOutlined />}
                        className={styles.snapshotButton}
                        value={x}>
                        <span>
                            {defaultFormat(x.createdAt)}
                        </span>
                    </Radio.Button>
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