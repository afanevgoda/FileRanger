import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import 'antd/dist/antd.css';
import SnapshotBrowser from '../src/components/SnapshotBrowser/SnapshotBrowser';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route path='/snapshot-browser' component={SnapshotBrowser} />
      </Layout>
    );
  }
}
