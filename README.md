# FileRanger

FileRanger is a web-based directory explorer. It contains of several services for different features like:

* Creating a snapshot of your folder and file structure
* Instant search
* Realtime folder browsing

Implemented features so far:

- [x] Create snapshot
- [x] Get a list of snapshots
- [ ] Scan your folder and files structure in real-time
- [ ] Data replication DB -> ES
- [ ] Search
- [ ] Everything in Docker

Architecture:

* Green = implemented
* Yellow = In progress
* Red = not started yet

![File Ranger Architecture](https://github.com/afanevgoda/FileRanger/raw/main/FileRanger%20Architecture.jpg)

Known bugs:

- [x] Deleting snapshot while it is in progress crashes the **SnapshotFileBrowser** service
- [ ] Race condition in Snapshot browser with files