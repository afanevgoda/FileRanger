import React, { useState } from "react";
import { Divider, Tooltip, Button, Space, Row, Col, Modal } from 'antd';
import { CloseOutlined, DeleteOutlined } from '@ant-design/icons';
import { defaultFormat } from '../../helpers/dateHelper';
import '../../components/SnapshotBrowser/SnapshotSelector.module.css';
import { deleteSnapshot } from "../../services/SnapshotService";

export default function SnapshotSelector({ snapshots, setSelectedSnapshot, selectedSnapshotId }) {
    const onClick = (snapshot) => setSelectedSnapshot(snapshot);

    const modal = ({ id, createdAt, drive, hostname }) => {
        console.log(id);
        Modal.confirm({
            title: `Do you want to delete snapshot ${defaultFormat(createdAt)} for ${hostname} - ${drive}`,
            onOk: () => deleteSnapshot(id),
            content: "All data for this snapshot will be removed",
            okText: "Delete",
            cancelText: "Cancel",
            icon: (<DeleteOutlined />),
            okButtonProps: {
                danger: true
            }
        });
    };

    let comp;
    if (snapshots.length == 0) {
        comp = <span>No snapshots found</span>
    } else {
        comp = <>
            <Row gutter={[4, 4]} justify="start">
                {snapshots.map(x => {
                    let tooltipText;
                    if (x.result === 0) tooltipText = "Scanning in progress. Snapshot can be incomplete"
                    else if (x.result === 1) tooltipText = "Scanning failed. The snapshot can be incomplete or broken"
                    else if (x.result === 2) tooltipText = "Everything is ok"

                    return (
                        <Col className="gutter-row" span={4}>
                            <Tooltip title={tooltipText} mouseEnterDelay={0.5}>
                                <Button
                                    type={x.result === 0 ? "dashed" : "primary"}
                                    onClick={() => onClick(x)}
                                    disabled={selectedSnapshotId === x.id}
                                    danger={x.result === 1}
                                >
                                    {defaultFormat(x.createdAt)}
                                </Button>
                            </Tooltip>
                            <Tooltip title="Delete this snapshot" mouseEnterDelay={0.5}>
                                <Button icon={<CloseOutlined />} onClick={() => modal(x)} />
                            </Tooltip>
                        </Col>
                    );
                })}
            </Row>
        </>
    }

    return (
        <>
            <Divider orientation="left">Select snapshot</Divider>
            <div>
                {comp}
            </div>
        </>
    )
}