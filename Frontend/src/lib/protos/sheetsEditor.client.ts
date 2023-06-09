// @generated by protobuf-ts 2.8.3
// @generated from protobuf file "sheetsEditor.proto" (package "sheets", syntax proto3)
// tslint:disable
import type { RpcTransport } from "@protobuf-ts/runtime-rpc";
import type { ServiceInfo } from "@protobuf-ts/runtime-rpc";
import { SheetsService } from "./sheetsEditor";
import type { RitAsPDF } from "./sheetsEditor";
import type { GetRitPDFRequest } from "./sheetsEditor";
import { stackIntercept } from "@protobuf-ts/runtime-rpc";
import type { PitAsPDF } from "./sheetsEditor";
import type { GetPitPDFRequest } from "./sheetsEditor";
import type { UnaryCall } from "@protobuf-ts/runtime-rpc";
import type { RpcOptions } from "@protobuf-ts/runtime-rpc";
/**
 * @generated from protobuf service sheets.SheetsService
 */
export interface ISheetsServiceClient {
    /**
     * @generated from protobuf rpc: GetPitPDF(sheets.GetPitPDFRequest) returns (sheets.PitAsPDF);
     */
    getPitPDF(input: GetPitPDFRequest, options?: RpcOptions): UnaryCall<GetPitPDFRequest, PitAsPDF>;
    /**
     * @generated from protobuf rpc: GetRitPDF(sheets.GetRitPDFRequest) returns (sheets.RitAsPDF);
     */
    getRitPDF(input: GetRitPDFRequest, options?: RpcOptions): UnaryCall<GetRitPDFRequest, RitAsPDF>;
}
/**
 * @generated from protobuf service sheets.SheetsService
 */
export class SheetsServiceClient implements ISheetsServiceClient, ServiceInfo {
    typeName = SheetsService.typeName;
    methods = SheetsService.methods;
    options = SheetsService.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: GetPitPDF(sheets.GetPitPDFRequest) returns (sheets.PitAsPDF);
     */
    getPitPDF(input: GetPitPDFRequest, options?: RpcOptions): UnaryCall<GetPitPDFRequest, PitAsPDF> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetPitPDFRequest, PitAsPDF>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetRitPDF(sheets.GetRitPDFRequest) returns (sheets.RitAsPDF);
     */
    getRitPDF(input: GetRitPDFRequest, options?: RpcOptions): UnaryCall<GetRitPDFRequest, RitAsPDF> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetRitPDFRequest, RitAsPDF>("unary", this._transport, method, opt, input);
    }
}
