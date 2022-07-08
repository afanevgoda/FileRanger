import React, { useState, useEffect } from "react";
import SnapshotSelector from './SnapshotSelector';
import ScannerSelector from './ScannerSelector';
import SnapshotStarter from './SnapshotStarter';
import DeleteModal from './DeleteModal';
import { getScanners } from '../../services/ScannerService';
import { getSnapshots, getFolders, getFiles } from '../../services/SnapshotService';
import FileBrowser from '../../components/FileBrowser/FileBrowser';
import { Divider, Typography } from "antd";

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
    const [filesAreLoading, setFilesAreLoading] = useState(false);
    const [foldersAreLoading, setFoldersAreLoading] = useState(false);

    useEffect(() => {
        const request = getScanners();
        request.then(x => {
            setScanners(x);
        })
    }, [])

    useEffect(() => {
        updateSnapshotList();
        const interval = setInterval(() => {
            setFilesAreLoading(true);
            setFoldersAreLoading(true);
            updateContent();
            updateSnapshotList()
        }, 10000);
        return () => clearInterval(interval)
    }, [selectedScanner, selectedDrive, targetPath])

    const updateSnapshotList = () => {
        if (selectedDrive === null || selectedScanner === null)
            return;
        const request = getSnapshots(selectedScanner?.hostName, selectedDrive);
        request.then(x => {
            setSnapshots(x);
        })
    }

    const updateContent = () => {
        if (selectedDrive === null || selectedScanner === null)
            return;
        const folderRequest = getFolders(targetPath, selectedSnapshot.id);
        const fileRequest = getFiles(targetPath, selectedSnapshot.id);
        Promise.all([folderRequest, fileRequest]).then((values) => {
            setFiles(values[1]);
            setFolders(values[0]);
            setFilesAreLoading(false);
            setFoldersAreLoading(false);
        })
    }

    useEffect(() => {
        if (targetPath === undefined || selectedSnapshot === undefined)
            return;
        setFiles([]);
        setFolders([]);
        setFilesAreLoading(true);
        setFoldersAreLoading(true);
        const folderRequest = getFolders(targetPath, selectedSnapshot.id);
        folderRequest.then(x => {
            setFolders(x);
            setFoldersAreLoading(false);
        })
        const fileRequest = getFiles(targetPath, selectedSnapshot.id);
        fileRequest.then(x => {
            setFiles(x);
            setFilesAreLoading(false);
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
                targetPath={targetPath}
                filesAreLoading={filesAreLoading}
                foldersAreLoading={foldersAreLoading}
            />
        </>
    );

    return (
        <>
            <Typography.Title level={2}>
                Snapshot Browser
            </Typography.Title>
            <Divider orientation="left">Select host and drive</Divider>
            <ScannerSelector
                scanners={scanners}
                setSelectedScanner={setSelectedScanner}
                setSelectedDrive={setSelectedDrive}
                selectedDrive={selectedDrive}
                setTargetPath={setTargetPath} />
            <SnapshotStarter selectedDrive={selectedDrive} selectedScanner={selectedScanner} />
            <SnapshotSelector
                snapshots={snapshots}
                setSelectedSnapshot={setSelectedSnapshot}
                selectedSnapshotId={selectedSnapshot?.id}
                dyingSnapshots={dyingSnapshots}
                setDyingSnapshots={setDyingSnapshots}
            />
            {snapshotComp}
            <DeleteModal />
        </>
    )
}