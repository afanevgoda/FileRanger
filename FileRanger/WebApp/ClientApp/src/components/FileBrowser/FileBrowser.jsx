import React from "react";
import { Card, Button, Tooltip, Empty } from "antd";
import { isDrivePath } from "../../helpers/fileHelper";
import styles from "../FileBrowser/FileBrowser.module.css";

export default function FileBrowser({ folders, files, setTargetPath, targetPath }) {

    const onClick = (folder) => {
        setTargetPath(folder.fullPath);
    };

    const goBack = () => {
        let previousFolder = targetPath.slice(0, targetPath.lastIndexOf("\\"));

        if (isDrivePath(previousFolder))
            previousFolder += "\\";

        console.log(previousFolder);
        setTargetPath(previousFolder);
    }

    let goBackExtra = () => {
        if (isDrivePath(targetPath))
            return;

        return (<Button onClick={goBack}>Go Back</Button>);
    };

    const comp = folders.length === 0 && files.length === 0 && isDrivePath(targetPath) ? (<Empty />) :
        (
            <>
                <Card title={targetPath} extra={goBackExtra()}>
                    {folders.map((folder) => (
                        <Card.Grid className={styles.folder} onClick={() => onClick(folder)}>{folder.name}</Card.Grid>
                    ))}
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