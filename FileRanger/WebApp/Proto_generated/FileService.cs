// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: FileService.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from FileService.proto</summary>
public static partial class FileServiceReflection {

  #region Descriptor
  /// <summary>File descriptor for FileService.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static FileServiceReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChFGaWxlU2VydmljZS5wcm90bxoTRm9sZGVyU2VydmljZS5wcm90byKGAQoE",
          "RmlsZRIKCgJJZBgBIAEoBRIMCgROYW1lGAIgASgJEhAKCEZ1bGxQYXRoGAMg",
          "ASgJEhIKClBhcmVudFBhdGgYBCABKAkSEQoJRXh0ZW5zaW9uGAUgASgJEhIK",
          "ClNuYXBzaG90SWQYBiABKAkSFwoGU3RhdHVzGAcgASgOMgcuU3RhdHVzIiMK",
          "C0xpc3RPZkZpbGVzEhQKBWZpbGVzGAEgAygLMgUuRmlsZSI9ChNHZXRGaWxl",
          "c0ZvclNuYXBzaG90EhIKClRhcmdldFBhdGgYASABKAkSEgoKU25hcHNob3RJ",
          "ZBgCIAEoBTJjCgtGaWxlU2VydmljZRIkCglTYXZlRmlsZXMSDC5MaXN0T2ZG",
          "aWxlcxoJLlJlc3BvbnNlEi4KCEdldEZpbGVzEhQuR2V0RmlsZXNGb3JTbmFw",
          "c2hvdBoMLkxpc3RPZkZpbGVzYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::FolderServiceReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::File), global::File.Parser, new[]{ "Id", "Name", "FullPath", "ParentPath", "Extension", "SnapshotId", "Status" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::ListOfFiles), global::ListOfFiles.Parser, new[]{ "Files" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::GetFilesForSnapshot), global::GetFilesForSnapshot.Parser, new[]{ "TargetPath", "SnapshotId" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class File : pb::IMessage<File>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<File> _parser = new pb::MessageParser<File>(() => new File());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<File> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::FileServiceReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public File() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public File(File other) : this() {
    id_ = other.id_;
    name_ = other.name_;
    fullPath_ = other.fullPath_;
    parentPath_ = other.parentPath_;
    extension_ = other.extension_;
    snapshotId_ = other.snapshotId_;
    status_ = other.status_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public File Clone() {
    return new File(this);
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "Name" field.</summary>
  public const int NameFieldNumber = 2;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "FullPath" field.</summary>
  public const int FullPathFieldNumber = 3;
  private string fullPath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string FullPath {
    get { return fullPath_; }
    set {
      fullPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "ParentPath" field.</summary>
  public const int ParentPathFieldNumber = 4;
  private string parentPath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string ParentPath {
    get { return parentPath_; }
    set {
      parentPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Extension" field.</summary>
  public const int ExtensionFieldNumber = 5;
  private string extension_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string Extension {
    get { return extension_; }
    set {
      extension_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "SnapshotId" field.</summary>
  public const int SnapshotIdFieldNumber = 6;
  private string snapshotId_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string SnapshotId {
    get { return snapshotId_; }
    set {
      snapshotId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Status" field.</summary>
  public const int StatusFieldNumber = 7;
  private global::Status status_ = global::Status.Ok;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public global::Status Status {
    get { return status_; }
    set {
      status_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as File);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(File other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Name != other.Name) return false;
    if (FullPath != other.FullPath) return false;
    if (ParentPath != other.ParentPath) return false;
    if (Extension != other.Extension) return false;
    if (SnapshotId != other.SnapshotId) return false;
    if (Status != other.Status) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (FullPath.Length != 0) hash ^= FullPath.GetHashCode();
    if (ParentPath.Length != 0) hash ^= ParentPath.GetHashCode();
    if (Extension.Length != 0) hash ^= Extension.GetHashCode();
    if (SnapshotId.Length != 0) hash ^= SnapshotId.GetHashCode();
    if (Status != global::Status.Ok) hash ^= Status.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (FullPath.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(FullPath);
    }
    if (ParentPath.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(ParentPath);
    }
    if (Extension.Length != 0) {
      output.WriteRawTag(42);
      output.WriteString(Extension);
    }
    if (SnapshotId.Length != 0) {
      output.WriteRawTag(50);
      output.WriteString(SnapshotId);
    }
    if (Status != global::Status.Ok) {
      output.WriteRawTag(56);
      output.WriteEnum((int) Status);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Name.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Name);
    }
    if (FullPath.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(FullPath);
    }
    if (ParentPath.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(ParentPath);
    }
    if (Extension.Length != 0) {
      output.WriteRawTag(42);
      output.WriteString(Extension);
    }
    if (SnapshotId.Length != 0) {
      output.WriteRawTag(50);
      output.WriteString(SnapshotId);
    }
    if (Status != global::Status.Ok) {
      output.WriteRawTag(56);
      output.WriteEnum((int) Status);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (FullPath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(FullPath);
    }
    if (ParentPath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(ParentPath);
    }
    if (Extension.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Extension);
    }
    if (SnapshotId.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(SnapshotId);
    }
    if (Status != global::Status.Ok) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(File other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.FullPath.Length != 0) {
      FullPath = other.FullPath;
    }
    if (other.ParentPath.Length != 0) {
      ParentPath = other.ParentPath;
    }
    if (other.Extension.Length != 0) {
      Extension = other.Extension;
    }
    if (other.SnapshotId.Length != 0) {
      SnapshotId = other.SnapshotId;
    }
    if (other.Status != global::Status.Ok) {
      Status = other.Status;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 26: {
          FullPath = input.ReadString();
          break;
        }
        case 34: {
          ParentPath = input.ReadString();
          break;
        }
        case 42: {
          Extension = input.ReadString();
          break;
        }
        case 50: {
          SnapshotId = input.ReadString();
          break;
        }
        case 56: {
          Status = (global::Status) input.ReadEnum();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Name = input.ReadString();
          break;
        }
        case 26: {
          FullPath = input.ReadString();
          break;
        }
        case 34: {
          ParentPath = input.ReadString();
          break;
        }
        case 42: {
          Extension = input.ReadString();
          break;
        }
        case 50: {
          SnapshotId = input.ReadString();
          break;
        }
        case 56: {
          Status = (global::Status) input.ReadEnum();
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class ListOfFiles : pb::IMessage<ListOfFiles>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<ListOfFiles> _parser = new pb::MessageParser<ListOfFiles>(() => new ListOfFiles());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<ListOfFiles> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::FileServiceReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ListOfFiles() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ListOfFiles(ListOfFiles other) : this() {
    files_ = other.files_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public ListOfFiles Clone() {
    return new ListOfFiles(this);
  }

  /// <summary>Field number for the "files" field.</summary>
  public const int FilesFieldNumber = 1;
  private static readonly pb::FieldCodec<global::File> _repeated_files_codec
      = pb::FieldCodec.ForMessage(10, global::File.Parser);
  private readonly pbc::RepeatedField<global::File> files_ = new pbc::RepeatedField<global::File>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::File> Files {
    get { return files_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as ListOfFiles);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(ListOfFiles other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!files_.Equals(other.files_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= files_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    files_.WriteTo(output, _repeated_files_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    files_.WriteTo(ref output, _repeated_files_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    size += files_.CalculateSize(_repeated_files_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(ListOfFiles other) {
    if (other == null) {
      return;
    }
    files_.Add(other.files_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          files_.AddEntriesFrom(input, _repeated_files_codec);
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          files_.AddEntriesFrom(ref input, _repeated_files_codec);
          break;
        }
      }
    }
  }
  #endif

}

public sealed partial class GetFilesForSnapshot : pb::IMessage<GetFilesForSnapshot>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<GetFilesForSnapshot> _parser = new pb::MessageParser<GetFilesForSnapshot>(() => new GetFilesForSnapshot());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<GetFilesForSnapshot> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::FileServiceReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GetFilesForSnapshot() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GetFilesForSnapshot(GetFilesForSnapshot other) : this() {
    targetPath_ = other.targetPath_;
    snapshotId_ = other.snapshotId_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public GetFilesForSnapshot Clone() {
    return new GetFilesForSnapshot(this);
  }

  /// <summary>Field number for the "TargetPath" field.</summary>
  public const int TargetPathFieldNumber = 1;
  private string targetPath_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string TargetPath {
    get { return targetPath_; }
    set {
      targetPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "SnapshotId" field.</summary>
  public const int SnapshotIdFieldNumber = 2;
  private int snapshotId_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int SnapshotId {
    get { return snapshotId_; }
    set {
      snapshotId_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as GetFilesForSnapshot);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(GetFilesForSnapshot other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (TargetPath != other.TargetPath) return false;
    if (SnapshotId != other.SnapshotId) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (TargetPath.Length != 0) hash ^= TargetPath.GetHashCode();
    if (SnapshotId != 0) hash ^= SnapshotId.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (TargetPath.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(TargetPath);
    }
    if (SnapshotId != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(SnapshotId);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (TargetPath.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(TargetPath);
    }
    if (SnapshotId != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(SnapshotId);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (TargetPath.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(TargetPath);
    }
    if (SnapshotId != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(SnapshotId);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(GetFilesForSnapshot other) {
    if (other == null) {
      return;
    }
    if (other.TargetPath.Length != 0) {
      TargetPath = other.TargetPath;
    }
    if (other.SnapshotId != 0) {
      SnapshotId = other.SnapshotId;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          TargetPath = input.ReadString();
          break;
        }
        case 16: {
          SnapshotId = input.ReadInt32();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          TargetPath = input.ReadString();
          break;
        }
        case 16: {
          SnapshotId = input.ReadInt32();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
