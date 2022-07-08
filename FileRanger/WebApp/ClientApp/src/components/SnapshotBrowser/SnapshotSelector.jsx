import React, { useState } from "react";
import { Divider, Tooltip, Button, Space, Row, Col, Modal } from 'antd';
import { CloseOutlined, DeleteOutlined, LoadingOutlined } from '@ant-design/icons';
import { defaultFormat } from '../../helpers/dateHelper';
import '../../components/SnapshotBrowser/SnapshotSelector.module.css';
import { deleteSnapshot } from "../../services/SnapshotService";

export default function SnapshotSelector({ snapshots, setSelectedSnapshot, selectedSnapshotId, dyingSnapshots, setDyingSnapshots }) {
    const onClick = (snapshot) => setSelectedSnapshot(snapshot);

    if (dyingSnapshots === undefined) {
        dyingSnapshots = [];
    }
    const modal = ({ id, createdAt, drive, hostname }) => {
        Modal.confirm({
            title: `Do you want to delete snapshot ${defaultFormat(createdAt)} for ${hostname} - ${drive}`,
            onOk: () => {
                setDyingSnapshots([...dyingSnapshots, id])
                deleteSnapshot(id);
            },
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
                    else if (dyingSnapshots.indexOf(x.id) >= 0) tooltipText = "The snapshot will be deleted soon"

                    return (
                        <Col className="gutter-row" span={4}>
                            <Tooltip title={tooltipText} mouseEnterDelay={0.5}>
                                <Button
                                    type={x.result === 0 ? "dashed" : "primary"}
                                    onClick={() => onClick(x)}
                                    disabled={selectedSnapshotId === x.id || dyingSnapshots.indexOf(x.id) >= 0}
                                    danger={x.result === 1}
                                >
                                    {defaultFormat(x.createdAt)}
                                </Button>
                            </Tooltip>
                            <Tooltip title="Delete this snapshot" mouseEnterDelay={0.5}>
                                <Button
                                    disabled={dyingSnapshots.indexOf(x.id) >= 0}
                                    icon={dyingSnapshots.indexOf(x.id) >= 0 ? <LoadingOutlined /> : <CloseOutlined />}
                                    onClick={() => modal(x)} />
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