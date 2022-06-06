import React, { useEffect, useState } from "react";
import { Button, Divider } from "antd";
import { startScan } from "../../services/ScannerService";

export default function SnapshotStarter({ selectedDrive }) {

    const defaultText = "Create new snapshot";
    const startingText = "Starting...";
    const failedTest = "Failed";
    const scanStartedText = "Started successfully";
    const [loading, setLoading] = useState(false);
    const [buttonText, setButtonText] = useState(defaultText);

    const onClick = () => {
        setLoading(true);
        const request = startScan(selectedDrive);
        request.then(_ => {
            setButtonText(scanStartedText)
            setLoading(true)
            resetButton();
        }, _ => {
            setButtonText(failedTest);
            setLoading(true);
            resetButton();
        });
    }

    const resetButton = () => setTimeout(() => {
        setButtonText(defaultText);
        setLoading(false)
    }, 5000);

    return (<>
        <Divider type="vertical"></Divider>
        <Button loading={loading} onClick={onClick} disabled={selectedDrive == null}>{buttonText}</Button>
    </>);
}