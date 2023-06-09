// @generated by protobuf-ts 2.8.3
// @generated from protobuf file "mail.proto" (package "sheets", syntax proto3)
// tslint:disable
import { ServiceType } from "@protobuf-ts/runtime-rpc";
import type { BinaryWriteOptions } from "@protobuf-ts/runtime";
import type { IBinaryWriter } from "@protobuf-ts/runtime";
import { WireType } from "@protobuf-ts/runtime";
import type { BinaryReadOptions } from "@protobuf-ts/runtime";
import type { IBinaryReader } from "@protobuf-ts/runtime";
import { UnknownFieldHandler } from "@protobuf-ts/runtime";
import type { PartialMessage } from "@protobuf-ts/runtime";
import { reflectionMergePartial } from "@protobuf-ts/runtime";
import { MESSAGE_TYPE } from "@protobuf-ts/runtime";
import { MessageType } from "@protobuf-ts/runtime";
/**
 * @generated from protobuf message sheets.EmailMessage
 */
export interface EmailMessage {
    /**
     * @generated from protobuf field: optional string from = 1;
     */
    from?: string;
    /**
     * @generated from protobuf field: optional string name = 2;
     */
    name?: string;
    /**
     * @generated from protobuf field: sheets.Type type = 3;
     */
    type: Type;
    /**
     * @generated from protobuf field: string message = 4;
     */
    message: string;
}
/**
 * @generated from protobuf message sheets.Result
 */
export interface Result {
    /**
     * @generated from protobuf field: bool success = 1;
     */
    success: boolean;
    /**
     * @generated from protobuf field: string message = 2;
     */
    message: string;
}
/**
 * @generated from protobuf enum sheets.Type
 */
export enum Type {
    /**
     * @generated from protobuf enum value: bug = 0;
     */
    bug = 0,
    /**
     * @generated from protobuf enum value: feature = 1;
     */
    feature = 1,
    /**
     * @generated from protobuf enum value: question = 2;
     */
    question = 2
}
// @generated message type with reflection information, may provide speed optimized methods
class EmailMessage$Type extends MessageType<EmailMessage> {
    constructor() {
        super("sheets.EmailMessage", [
            { no: 1, name: "from", kind: "scalar", opt: true, T: 9 /*ScalarType.STRING*/ },
            { no: 2, name: "name", kind: "scalar", opt: true, T: 9 /*ScalarType.STRING*/ },
            { no: 3, name: "type", kind: "enum", T: () => ["sheets.Type", Type] },
            { no: 4, name: "message", kind: "scalar", T: 9 /*ScalarType.STRING*/ }
        ]);
    }
    create(value?: PartialMessage<EmailMessage>): EmailMessage {
        const message = { type: 0, message: "" };
        globalThis.Object.defineProperty(message, MESSAGE_TYPE, { enumerable: false, value: this });
        if (value !== undefined)
            reflectionMergePartial<EmailMessage>(this, message, value);
        return message;
    }
    internalBinaryRead(reader: IBinaryReader, length: number, options: BinaryReadOptions, target?: EmailMessage): EmailMessage {
        let message = target ?? this.create(), end = reader.pos + length;
        while (reader.pos < end) {
            let [fieldNo, wireType] = reader.tag();
            switch (fieldNo) {
                case /* optional string from */ 1:
                    message.from = reader.string();
                    break;
                case /* optional string name */ 2:
                    message.name = reader.string();
                    break;
                case /* sheets.Type type */ 3:
                    message.type = reader.int32();
                    break;
                case /* string message */ 4:
                    message.message = reader.string();
                    break;
                default:
                    let u = options.readUnknownField;
                    if (u === "throw")
                        throw new globalThis.Error(`Unknown field ${fieldNo} (wire type ${wireType}) for ${this.typeName}`);
                    let d = reader.skip(wireType);
                    if (u !== false)
                        (u === true ? UnknownFieldHandler.onRead : u)(this.typeName, message, fieldNo, wireType, d);
            }
        }
        return message;
    }
    internalBinaryWrite(message: EmailMessage, writer: IBinaryWriter, options: BinaryWriteOptions): IBinaryWriter {
        /* optional string from = 1; */
        if (message.from !== undefined)
            writer.tag(1, WireType.LengthDelimited).string(message.from);
        /* optional string name = 2; */
        if (message.name !== undefined)
            writer.tag(2, WireType.LengthDelimited).string(message.name);
        /* sheets.Type type = 3; */
        if (message.type !== 0)
            writer.tag(3, WireType.Varint).int32(message.type);
        /* string message = 4; */
        if (message.message !== "")
            writer.tag(4, WireType.LengthDelimited).string(message.message);
        let u = options.writeUnknownFields;
        if (u !== false)
            (u == true ? UnknownFieldHandler.onWrite : u)(this.typeName, message, writer);
        return writer;
    }
}
/**
 * @generated MessageType for protobuf message sheets.EmailMessage
 */
export const EmailMessage = new EmailMessage$Type();
// @generated message type with reflection information, may provide speed optimized methods
class Result$Type extends MessageType<Result> {
    constructor() {
        super("sheets.Result", [
            { no: 1, name: "success", kind: "scalar", T: 8 /*ScalarType.BOOL*/ },
            { no: 2, name: "message", kind: "scalar", T: 9 /*ScalarType.STRING*/ }
        ]);
    }
    create(value?: PartialMessage<Result>): Result {
        const message = { success: false, message: "" };
        globalThis.Object.defineProperty(message, MESSAGE_TYPE, { enumerable: false, value: this });
        if (value !== undefined)
            reflectionMergePartial<Result>(this, message, value);
        return message;
    }
    internalBinaryRead(reader: IBinaryReader, length: number, options: BinaryReadOptions, target?: Result): Result {
        let message = target ?? this.create(), end = reader.pos + length;
        while (reader.pos < end) {
            let [fieldNo, wireType] = reader.tag();
            switch (fieldNo) {
                case /* bool success */ 1:
                    message.success = reader.bool();
                    break;
                case /* string message */ 2:
                    message.message = reader.string();
                    break;
                default:
                    let u = options.readUnknownField;
                    if (u === "throw")
                        throw new globalThis.Error(`Unknown field ${fieldNo} (wire type ${wireType}) for ${this.typeName}`);
                    let d = reader.skip(wireType);
                    if (u !== false)
                        (u === true ? UnknownFieldHandler.onRead : u)(this.typeName, message, fieldNo, wireType, d);
            }
        }
        return message;
    }
    internalBinaryWrite(message: Result, writer: IBinaryWriter, options: BinaryWriteOptions): IBinaryWriter {
        /* bool success = 1; */
        if (message.success !== false)
            writer.tag(1, WireType.Varint).bool(message.success);
        /* string message = 2; */
        if (message.message !== "")
            writer.tag(2, WireType.LengthDelimited).string(message.message);
        let u = options.writeUnknownFields;
        if (u !== false)
            (u == true ? UnknownFieldHandler.onWrite : u)(this.typeName, message, writer);
        return writer;
    }
}
/**
 * @generated MessageType for protobuf message sheets.Result
 */
export const Result = new Result$Type();
/**
 * @generated ServiceType for protobuf service sheets.EmailService
 */
export const EmailService = new ServiceType("sheets.EmailService", [
    { name: "SendEmail", options: {}, I: EmailMessage, O: Result }
], { "ts.server": ["GRPC1_SERVER"] });
