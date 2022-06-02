import React, { useState } from "react";
import { Divider, Tooltip, Button, Space, Dropdown } from 'antd';
import { defaultFormat } from '../../helpers/dateHelper';
import '../../components/SnapshotBrowser/SnapshotSelector.module.css';

export default function SnapshotSelector({ snapshots, setSelectedSnapshot, selectedSnapshotId }) {
    const onClick = (snapshot) => setSelectedSnapshot(snapshot);

    let comp;
    if (snapshots.length == 0) {
        comp = <span>No snapshots found</span>
    } else {
        comp = <>
            {/* <Radio.Group> */}
            {snapshots.map(x => {
                let tooltipText;
                if (x.result === 0) tooltipText = "Scanning failed. The snapshot can be incomplete or broken"
                else if (x.result === 1) tooltipText = "Scanning in progress. Snapshot can be incomplete"
                else if (x.result === 2) tooltipText = "Everything is ok"


                return (
                    <Tooltip title={tooltipText} mouseEnterDelay={0.5}>
                        <Button
                            type={x.result === 1 ? "dashed" : "primary"}
                            onClick={() => onClick(x)}
                            disabled={selectedSnapshotId === x.id}
                            danger={x.result === 0}

                        >
                            {defaultFormat(x.createdAt)}
                        </Button>
                    </Tooltip>
                    // <Radio.Button
                    //     icon={<FastBackwardOutlined />}
                    //     className={buttonClass}
                    //     value={x}
                    //     danger>
                    //     <span>
                    //         {defaultFormat(x.createdAt)}
                    //     </span>
                    // </Radio.Button>
                );
            })}
            {/* </Radio.Group> */}
        </>
    }

    return (
        <>
            <Divider type="vertical"></Divider>
            <Space>{comp}</Space>
        </>
    )
}