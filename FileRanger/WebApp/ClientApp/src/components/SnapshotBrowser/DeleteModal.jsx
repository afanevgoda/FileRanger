import React from "react";
import { Modal, Button, Space } from 'antd';

export default function DeleteModal({ targetSnapshot }) {

    const modal = () => {
        Modal.error({
            title: `Do you want to delete snapshot on`,
        });
    };

    return (<>
        <Button onClick={modal} />
    </>)
}