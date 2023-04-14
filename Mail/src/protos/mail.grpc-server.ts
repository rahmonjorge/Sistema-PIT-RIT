// @generated by protobuf-ts 2.8.3
// @generated from protobuf file "mail.proto" (package "sheets", syntax proto3)
// tslint:disable
import { Result } from "./mail";
import { EmailMessage } from "./mail";
import type * as grpc from "@grpc/grpc-js";
/**
 * @generated from protobuf service sheets.EmailService
 */
export interface IEmailService extends grpc.UntypedServiceImplementation {
    /**
     * @generated from protobuf rpc: SendEmail(sheets.EmailMessage) returns (sheets.Result);
     */
    sendEmail: grpc.handleUnaryCall<EmailMessage, Result>;
}
/**
 * @grpc/grpc-js definition for the protobuf service sheets.EmailService.
 *
 * Usage: Implement the interface IEmailService and add to a grpc server.
 *
 * ```typescript
 * const server = new grpc.Server();
 * const service: IEmailService = ...
 * server.addService(emailServiceDefinition, service);
 * ```
 */
export const emailServiceDefinition: grpc.ServiceDefinition<IEmailService> = {
    sendEmail: {
        path: "/sheets.EmailService/SendEmail",
        originalName: "SendEmail",
        requestStream: false,
        responseStream: false,
        responseDeserialize: bytes => Result.fromBinary(bytes),
        requestDeserialize: bytes => EmailMessage.fromBinary(bytes),
        responseSerialize: value => Buffer.from(Result.toBinary(value)),
        requestSerialize: value => Buffer.from(EmailMessage.toBinary(value))
    }
};
