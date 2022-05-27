import React from "react";
import { Card, Button } from "antd";
import { CaretLeftOutlined } from '@ant-design/icons';
import styles from "../FileBrowser/FileBrowser.module.css";

export default function FileBrowser({ folders, setTargetPath, targetPath }) {

    const onClick = (folder) => {
        setTargetPath(folder.fullPath);
    };

    const goBack = () => {
        let previousFolder = targetPath.slice(0, targetPath.lastIndexOf("\\"));

        if (previousFolder.indexOf("\\") === -1)
            previousFolder += "\\";

        console.log(previousFolder);
        setTargetPath(previousFolder);
    }

    const goBackExtra = (<><Button onClick={goBack}>Go Back</Button></>);

    return (<>
        <Card title={targetPath} extra={goBackExtra}>
            {folders.map((folder) => (
                <Card.Grid className={styles.folder} onClick={() => onClick(folder)}>{folder.name}</Card.Grid>
            ))}
        </Card>
    </>)
}