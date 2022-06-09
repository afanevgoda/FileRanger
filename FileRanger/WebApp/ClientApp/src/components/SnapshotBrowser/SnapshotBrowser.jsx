import React, { useState, useEffect } from "react";
import SnapshotSelector from './SnapshotSelector';
import ScannerSelector from './ScannerSelector';
import SnapshotStarter from './SnapshotStarter';
import DeleteModal from './DeleteModal';
import { getScanners } from '../../services/ScannerService';
import { getSnapshots, getFolders, getFiles } from '../../services/SnapshotService';
import FileBrowser from '../../components/FileBrowser/FileBrowser';
import { Divider } from "antd";

export default function SnapshotBrowser() {

    const [scanners, setScanners] = useState([]);
    const [selectedScanner, setSelectedScanner] = useState();
    const [selectedDrive, setSelectedDrive] = useState();
    const [snapshots, setSnapshots] = useState([]);
    const [selectedSnapshot, setSelectedSnapshot] = useState();
    const [targetPath, setTargetPath] = useState();
    const [folders, setFolders] = useState([]);
    const [files, setFiles] = useState([]);
    const [dyingSnapshots, setDyingSnapshots] = useState([]);

    useEffect(() => {
        const request = getScanners();
        request.then(x => {
            setScanners(x);
        })
    }, [])

    useEffect(() => {
        updateSnapshotList();
        const interval = setInterval(updateSnapshotList, 5000);
        return () => clearInterval(interval)
    }, [selectedScanner, selectedDrive])

    const updateSnapshotList = () => {
        if (selectedDrive === null || selectedScanner === null)
            return;
        const request = getSnapshots(selectedScanner, selectedDrive);
        request.then(x => {
            setSnapshots(x);
            // setTargetPath(selectedDrive);
        })
    }

    useEffect(() => {
        if (targetPath === undefined || selectedSnapshot === undefined)
            return;

        const folderRequest = getFolders(targetPath, selectedSnapshot.id);
        folderRequest.then(x => {
            setFolders(x);
        })
        const fileRequest = getFiles(targetPath, selectedSnapshot.id);
        fileRequest.then(x => {
            setFiles(x);
        })
    }, [selectedSnapshot, targetPath])

    const snapshotComp = (snapshots.length === 0 ?
        <></> :
        <>
            <Divider orientation="left">Content</Divider>
            <FileBrowser
                folders={folders}
                files={files}
                setTargetPath={setTargetPath}
                targetPath={targetPath} />
        </>
    );

    return (
        <>
            <Divider orientation="left">Select host and drive</Divider>
            <ScannerSelector
                scanners={scanners}
                setSelectedScanner={setSelectedScanner}
                setSelectedDrive={setSelectedDrive}
                selectedDrive={selectedDrive}
                setTargetPath={setTargetPath} />
            <SnapshotStarter selectedDrive={selectedDrive} />
            <SnapshotSelector
                snapshots={snapshots}
                setSelectedSnapshot={setSelectedSnapshot}
                selectedSnapshotId={selectedSnapshot?.id}
                dyingSnapshots={dyingSnapshots}
            />
            {snapshotComp}
            <DeleteModal />
        </>
    )
}