import React from "react";
import { Card, Button, Tooltip, Empty, Spin } from "antd";
import { isDrivePath } from "../../helpers/fileHelper";
import { FileOutlined, SyncOutlined, FolderOutlined, CheckCircleOutlined } from '@ant-design/icons';
import styles from "../FileBrowser/FileBrowser.module.css";

export default function FileBrowser({ folders, files, setTargetPath, targetPath, filesAreLoading, foldersAreLoading }) {

    const onClick = (folder) => {
        setTargetPath(folder.fullPath);
    };

    const goBack = () => {
        let previousFolder = targetPath.slice(0, targetPath.lastIndexOf("\\"));

        if (isDrivePath(previousFolder))
            previousFolder += "\\";

        setTargetPath(previousFolder);
    }

    const extraComp = () => {
        let goBackComp = <Button onClick={goBack}>Go Back</Button>;
        if (isDrivePath(targetPath))
            goBackComp = null;

        return (<>
            <Tooltip title={filesAreLoading ? "Files are loading..." : "Files are loaded"}>
                <FileOutlined />
                {filesAreLoading ? <SyncOutlined spin /> : <CheckCircleOutlined />}
            </Tooltip>
            <Tooltip title={foldersAreLoading ? "Folders are loading..." : "Folders are loaded"}>
                <FolderOutlined />
                {foldersAreLoading ? <SyncOutlined spin /> : <CheckCircleOutlined />}
            </Tooltip>
            {goBackComp}
        </>);
    }
    let goBackExtra = () => {
        if (isDrivePath(targetPath))
            return;

        return (<>
            <FileOutlined />
            <Spin />
            <Button onClick={goBack}>Go Back</Button>
        </>);
    };

    const comp = folders.length === 0 && files.length === 0 && isDrivePath(targetPath) ? (<Empty />) :
        (
            <>
                <Card title={targetPath} extra={extraComp()}>
                    {folders.map((folder) => {
                        const isFailed = folder.status != 0;
                        if (isFailed)
                            return (
                                <Tooltip title="Scanner could not manage to scan this folder" color="red">
                                    <Card.Grid className={styles.folderFailed} onClick={() => onClick(folder)}>{folder.name}</Card.Grid>
                                </Tooltip>
                            )
                        return (<Card.Grid className={styles.folder} onClick={() => onClick(folder)}>{folder.name}</Card.Grid>)
                    }

                    )}
                    {files.map((file) => (
                        <Tooltip title="This is a file. It cannot be opened" color="red" mouseEnterDelay={0.5}>
                            <Card.Grid className={styles.file}>{file.name}</Card.Grid>
                        </Tooltip>
                    ))}
                </Card>
            </>
        );

    return (comp)
}