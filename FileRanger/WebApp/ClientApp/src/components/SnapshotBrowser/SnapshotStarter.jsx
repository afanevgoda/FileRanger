import React, { useEffect, useState } from "react";
import { Button } from "antd";
import { startScan } from "../../services/ScannerService";

export default function SnapshotStarter({ selectedDrive }) {

    const [loading, setLoading] = useState(false);
    const [buttonText, setButtonText] = useState("Start scan");

    const onClick = () => {
        setLoading(true);
        const request = startScan(selectedDrive);
        request.then(_ => {
            setButtonText("Starting...")
            setLoading(true)
            resetButton();
        }, _ => {
            setButtonText("Failed");
            setLoading(true);
            resetButton();
        });
    }

    const resetButton = () => setTimeout(() => {
        setButtonText("Start scan");
        setLoading(false)
    }, 5000);

    return (<>
        <Button loading={loading} onClick={onClick}>{buttonText}</Button>
    </>);
}