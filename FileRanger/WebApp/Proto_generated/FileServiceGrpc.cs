// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: FileService.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

public static partial class FileService
{
  static readonly string __ServiceName = "FileService";

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (message is global::Google.Protobuf.IBufferMessage)
    {
      context.SetPayloadLength(message.CalculateSize());
      global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
      context.Complete();
      return;
    }
    #endif
    context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static class __Helper_MessageCache<T>
  {
    public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (__Helper_MessageCache<T>.IsBufferMessage)
    {
      return parser.ParseFrom(context.PayloadAsReadOnlySequence());
    }
    #endif
    return parser.ParseFrom(context.PayloadAsNewBuffer());
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::ListOfFiles> __Marshaller_ListOfFiles = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ListOfFiles.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Response> __Marshaller_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Response.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::GetFilesForSnapshot> __Marshaller_GetFilesForSnapshot = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GetFilesForSnapshot.Parser));

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::ListOfFiles, global::Response> __Method_SaveFiles = new grpc::Method<global::ListOfFiles, global::Response>(
      grpc::MethodType.Unary,
      __ServiceName,
      "SaveFiles",
      __Marshaller_ListOfFiles,
      __Marshaller_Response);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::GetFilesForSnapshot, global::ListOfFiles> __Method_GetFiles = new grpc::Method<global::GetFilesForSnapshot, global::ListOfFiles>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetFiles",
      __Marshaller_GetFilesForSnapshot,
      __Marshaller_ListOfFiles);

  /// <summary>Service descriptor</summary>
  public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
  {
    get { return global::FileServiceReflection.Descriptor.Services[0]; }
  }

  /// <summary>Client for FileService</summary>
  public partial class FileServiceClient : grpc::ClientBase<FileServiceClient>
  {
    /// <summary>Creates a new client for FileService</summary>
    /// <param name="channel">The channel to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public FileServiceClient(grpc::ChannelBase channel) : base(channel)
    {
    }
    /// <summary>Creates a new client for FileService that uses a custom <c>CallInvoker</c>.</summary>
    /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public FileServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
    {
    }
    /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected FileServiceClient() : base()
    {
    }
    /// <summary>Protected constructor to allow creation of configured clients.</summary>
    /// <param name="configuration">The client configuration.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected FileServiceClient(ClientBaseConfiguration configuration) : base(configuration)
    {
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Response SaveFiles(global::ListOfFiles request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return SaveFiles(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Response SaveFiles(global::ListOfFiles request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_SaveFiles, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Response> SaveFilesAsync(global::ListOfFiles request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return SaveFilesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Response> SaveFilesAsync(global::ListOfFiles request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_SaveFiles, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::ListOfFiles GetFiles(global::GetFilesForSnapshot request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetFiles(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::ListOfFiles GetFiles(global::GetFilesForSnapshot request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetFiles, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::ListOfFiles> GetFilesAsync(global::GetFilesForSnapshot request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetFilesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::ListOfFiles> GetFilesAsync(global::GetFilesForSnapshot request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetFiles, null, options, request);
    }
    /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected override FileServiceClient NewInstance(ClientBaseConfiguration configuration)
    {
      return new FileServiceClient(configuration);
    }
  }

}
#endregion
