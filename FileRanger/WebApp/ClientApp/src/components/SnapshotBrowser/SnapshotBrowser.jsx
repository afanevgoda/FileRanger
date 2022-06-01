import React, { useState, useEffect } from "react";
import SnapshotSelector from './SnapshotSelector';
import ScannerSelector from '../SnapshotBrowser/ScannerSelector';
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

    useEffect(() => {
        const request = getScanners();
        request.then(x => {
            setScanners(x);
        })
    }, [])

    useEffect(() => {
        if (selectedDrive === null || selectedScanner === null)
            return;
        const request = getSnapshots(selectedScanner, selectedDrive);
        request.then(x => {
            setSnapshots(x);
            setTargetPath(selectedDrive);
        })
    }, [selectedScanner, selectedDrive])

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
            <Divider>Content</Divider>
            <FileBrowser folders={folders} files={files} setTargetPath={setTargetPath} targetPath={targetPath} />
        </>
    );

    return (
        <>
            <ScannerSelector
                scanners={scanners}
                setSelectedScanner={setSelectedScanner}
                setSelectedDrive={setSelectedDrive}
                selectedDrive={selectedDrive} />
            <SnapshotSelector snapshots={snapshots} setSelectedSnapshot={setSelectedSnapshot} />
            {snapshotComp}
        </>
    )
}