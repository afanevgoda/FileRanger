import { React, useState, useEffect } from "react"
import { Treemap } from '@ant-design/plots';
import { Typography, Spin } from "antd";
import { getScanners } from '../../services/ScannerService';
import { getSnapshots, getFolders, getFiles } from '../../services/SnapshotService';
import ScannerSelector from '../../components/SnapshotBrowser/ScannerSelector';
import SnapshotSelector from '../../components/SnapshotBrowser/SnapshotSelector';

export default function FileSystemData() {
    const [data, setData] = useState([]);
    const [scanners, setScanners] = useState([]);
    const [selectedScanner, setSelectedScanner] = useState();
    const [selectedDrive, setSelectedDrive] = useState();
    const [snapshots, setSnapshots] = useState([]);
    const [selectedSnapshot, setSelectedSnapshot] = useState();
    const [targetPath, setTargetPath] = useState();
    const [loading, setLoading] = useState(false);
    const [config, setConfig] = useState({
        data,
        colorField: 'name',
        tooltip: {
            formatter: (v) => {
                const root = v.path[v.path.length - 1];
                let value;
                let extra;
                if (v.value >= 1000000) {
                    value = Math.floor(v.value / 1000000);
                    extra = "gb"
                }
                else if (v.value >= 1024) {
                    value = Math.floor(v.value / 1024);
                    extra = "mb";
                } else {
                    value = Math.floor(v.value);
                    extra = "kb";
                }
                const tooltip = value + extra;
                return {
                    name: v.name,
                    value: tooltip,
                };
            },
        },
        onReady: (plot) => {
            plot.on('element:click', (...args) => {
                console.log(args[0].data.data.fullPath);
                setTargetPath(args[0].data.data.fullPath);
            });
            plot.on('element:treemap-drill-down', (...args) => {
                console.log("drilled")
                console.log(args)
                // console.log(args[0].data.data.fullPath);
                // setTargetPath(args[0].data.data.fullPath);
            });

        },
    });

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
        const request = getSnapshots(selectedScanner?.hostName, selectedDrive);
        request.then(x => {
            setSnapshots(x);
            setTargetPath(selectedDrive);
        })
    }

    useEffect(_ => {
        if (targetPath === undefined || selectedSnapshot === undefined)
            return;
        setLoading(true);
        let foldersPromise = getFolders(targetPath, selectedSnapshot.id);
        let filesPromise = getFiles(targetPath, selectedSnapshot.id);
        Promise.all([foldersPromise, filesPromise]).then(values => {
            let rawData = [];
            rawData =
                values.flat(2).map(x => {
                    return {
                        name: x.name,
                        value: x.size,
                        fullPath: x.fullPath
                    };
                });
            var resultData = {
                name: "root",
                children: rawData,
            }
            setData(resultData);
            setLoading(false);
        })
    }, [targetPath, selectedSnapshot])

    useEffect(() => {
        setConfig({
            data,
            colorField: 'name',
            interactions: [
                {
                    type: 'treemap-drill-down',
                },
                {
                    type: 'click',
                },
            ],
        });
    }, [data])

    const treeMap = <Treemap {...config} />;;
    return (
        <>
            <Typography.Title level={2}>
                File System Data
            </Typography.Title>
            <ScannerSelector
                scanners={scanners}
                setSelectedScanner={setSelectedScanner}
                setSelectedDrive={setSelectedDrive}
                selectedDrive={selectedDrive}
                setTargetPath={setTargetPath} />
            <SnapshotSelector
                snapshots={snapshots}
                setSelectedSnapshot={setSelectedSnapshot}
                selectedSnapshotId={selectedSnapshot?.id}
            />
            <Spin spinning={loading}>
                {treeMap}
            </Spin>
        </>
    );
}