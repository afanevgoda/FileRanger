import React, { useEffect } from "react";
import { Menu, Dropdown, Button, Space, Divider } from 'antd';

export default function ScannerSelector({ scanners, setSelectedScanner, setSelectedDrive, selectedDrive }) {

    const onDriveSelect = (drive, host) => {
        setSelectedDrive(drive);
        setSelectedScanner(host);
    };

    const scannerDropdown = (hostName, drives) => {
        const kek = drives.map(x => ({
            key: x,
            label: x
        }));
        return (<Menu items={kek} onClick={({ key }) => onDriveSelect(key, hostName)}></Menu>)
    }

    const dropdowns = () => (
        scanners.map(x => (
            <Dropdown overlay={() => scannerDropdown(x.hostName, x.drives)} placement="bottomLeft">
                <Button>{x.hostName} {selectedDrive}</Button>
            </Dropdown>
        ))
    );

    return (
        <>
            {dropdowns()}
        </>
    )
}