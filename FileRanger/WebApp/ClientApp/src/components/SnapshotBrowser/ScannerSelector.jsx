import React, { useEffect } from "react";
import { Menu, Dropdown, Button, Space, Badge, Tooltip } from 'antd';
import { CheckCircleOutlined } from '@ant-design/icons';


export default function ScannerSelector({ scanners, setSelectedScanner, setSelectedDrive, selectedDrive, setTargetPath }) {

    const onDriveSelect = (drive, scanner) => {
        setSelectedDrive(drive);
        setSelectedScanner(scanner);
        setTargetPath(drive);
    };

    const scannerDropdown = (scanner, drives) => {
        const items = drives.map(x => ({
            key: x,
            label: x
        }));
        return (<Menu items={items} onClick={({ key }) => onDriveSelect(key, scanner)}></Menu>)
    }

    const getStatusColor = (scannerInfo) => {
        if (scannerInfo.status === 0)
            return "green";
        else if (scannerInfo.status === 1)
            return "yellow";
        else if (scannerInfo.status === 2)
            return "red";
    }

    const getTooltipColor = ({ status }) => {
        if (status === 0)
            return "green";
        else if (status === 1)
            return "yellow";
        else if (status === 2)
            return "red";
    }

    const getTooltipTitle = ({ status }) => {
        if (status === 0)
            return "Scanner is healthy";
        else if (status === 1)
            return "Couldn't access scanner recently";
        else if (status === 2)
            return "Scanner is disconnected";
    }

    const dropdowns = () => (
        scanners.map(x => (
            <Tooltip title={getTooltipTitle(x)} color={getTooltipColor(x)}>
                <Dropdown overlay={() => scannerDropdown(x, x.drives)} placement="bottomLeft">
                    <Button>
                        <Badge dot color={getStatusColor(x)}>
                            {x.hostName} {selectedDrive}
                        </Badge>
                    </Button>
                </Dropdown >
            </Tooltip>
        ))
    );

    return (
        <>
            {dropdowns()}
        </>
    )
}