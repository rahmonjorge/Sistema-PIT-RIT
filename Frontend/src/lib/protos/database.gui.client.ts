// @generated by protobuf-ts 2.8.3
// @generated from protobuf file "database.gui.proto" (package "database.gui", syntax proto3)
// tslint:disable
import { RitService } from "./database.gui";
import type { UpdateRitRequest } from "./database.gui";
import type { GetRitRequest } from "./database.gui";
import { PitService } from "./database.gui";
import type { UpdatePitRequest } from "./database.gui";
import type { GetPitRequest } from "./database.gui";
import type { Sheet } from "./database.gui";
import type { CreatePitRequest } from "./database.gui";
import type { RpcTransport } from "@protobuf-ts/runtime-rpc";
import type { ServiceInfo } from "@protobuf-ts/runtime-rpc";
import { UserService } from "./database.gui";
import type { Anos } from "./database.gui";
import type { UserIdRequest } from "./database.gui";
import type { UpdateUserInfoRequest } from "./database.gui";
import { stackIntercept } from "@protobuf-ts/runtime-rpc";
import type { UserInfo } from "./database.gui";
import type { CompletarCadastroRequest } from "./database.gui";
import type { UnaryCall } from "@protobuf-ts/runtime-rpc";
import type { RpcOptions } from "@protobuf-ts/runtime-rpc";
/**
 * @generated from protobuf service database.gui.UserService
 */
export interface IUserServiceClient {
    /**
     * @generated from protobuf rpc: CompletarCadastro(database.gui.CompletarCadastroRequest) returns (database.gui.UserInfo);
     */
    completarCadastro(input: CompletarCadastroRequest, options?: RpcOptions): UnaryCall<CompletarCadastroRequest, UserInfo>;
    /**
     * @generated from protobuf rpc: UpdateUserInfo(database.gui.UpdateUserInfoRequest) returns (database.gui.UserInfo);
     */
    updateUserInfo(input: UpdateUserInfoRequest, options?: RpcOptions): UnaryCall<UpdateUserInfoRequest, UserInfo>;
    /**
     * @generated from protobuf rpc: GetUserInfo(database.gui.UserIdRequest) returns (database.gui.UserInfo);
     */
    getUserInfo(input: UserIdRequest, options?: RpcOptions): UnaryCall<UserIdRequest, UserInfo>;
    /**
     * @generated from protobuf rpc: GetAnosFromUser(database.gui.UserIdRequest) returns (database.gui.Anos);
     */
    getAnosFromUser(input: UserIdRequest, options?: RpcOptions): UnaryCall<UserIdRequest, Anos>;
}
/**
 * @generated from protobuf service database.gui.UserService
 */
export class UserServiceClient implements IUserServiceClient, ServiceInfo {
    typeName = UserService.typeName;
    methods = UserService.methods;
    options = UserService.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CompletarCadastro(database.gui.CompletarCadastroRequest) returns (database.gui.UserInfo);
     */
    completarCadastro(input: CompletarCadastroRequest, options?: RpcOptions): UnaryCall<CompletarCadastroRequest, UserInfo> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<CompletarCadastroRequest, UserInfo>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UpdateUserInfo(database.gui.UpdateUserInfoRequest) returns (database.gui.UserInfo);
     */
    updateUserInfo(input: UpdateUserInfoRequest, options?: RpcOptions): UnaryCall<UpdateUserInfoRequest, UserInfo> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<UpdateUserInfoRequest, UserInfo>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetUserInfo(database.gui.UserIdRequest) returns (database.gui.UserInfo);
     */
    getUserInfo(input: UserIdRequest, options?: RpcOptions): UnaryCall<UserIdRequest, UserInfo> {
        const method = this.methods[2], opt = this._transport.mergeOptions(options);
        return stackIntercept<UserIdRequest, UserInfo>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetAnosFromUser(database.gui.UserIdRequest) returns (database.gui.Anos);
     */
    getAnosFromUser(input: UserIdRequest, options?: RpcOptions): UnaryCall<UserIdRequest, Anos> {
        const method = this.methods[3], opt = this._transport.mergeOptions(options);
        return stackIntercept<UserIdRequest, Anos>("unary", this._transport, method, opt, input);
    }
}
/**
 * @generated from protobuf service database.gui.PitService
 */
export interface IPitServiceClient {
    /**
     * @generated from protobuf rpc: CreatePit(database.gui.CreatePitRequest) returns (database.gui.Sheet);
     */
    createPit(input: CreatePitRequest, options?: RpcOptions): UnaryCall<CreatePitRequest, Sheet>;
    /**
     * @generated from protobuf rpc: GetPit(database.gui.GetPitRequest) returns (database.gui.Sheet);
     */
    getPit(input: GetPitRequest, options?: RpcOptions): UnaryCall<GetPitRequest, Sheet>;
    /**
     * @generated from protobuf rpc: UpdatePit(database.gui.UpdatePitRequest) returns (database.gui.Sheet);
     */
    updatePit(input: UpdatePitRequest, options?: RpcOptions): UnaryCall<UpdatePitRequest, Sheet>;
}
/**
 * @generated from protobuf service database.gui.PitService
 */
export class PitServiceClient implements IPitServiceClient, ServiceInfo {
    typeName = PitService.typeName;
    methods = PitService.methods;
    options = PitService.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CreatePit(database.gui.CreatePitRequest) returns (database.gui.Sheet);
     */
    createPit(input: CreatePitRequest, options?: RpcOptions): UnaryCall<CreatePitRequest, Sheet> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<CreatePitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetPit(database.gui.GetPitRequest) returns (database.gui.Sheet);
     */
    getPit(input: GetPitRequest, options?: RpcOptions): UnaryCall<GetPitRequest, Sheet> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetPitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UpdatePit(database.gui.UpdatePitRequest) returns (database.gui.Sheet);
     */
    updatePit(input: UpdatePitRequest, options?: RpcOptions): UnaryCall<UpdatePitRequest, Sheet> {
        const method = this.methods[2], opt = this._transport.mergeOptions(options);
        return stackIntercept<UpdatePitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
}
/**
 * @generated from protobuf service database.gui.RitService
 */
export interface IRitServiceClient {
    /**
     * @generated from protobuf rpc: CreateRit(database.gui.CreatePitRequest) returns (database.gui.Sheet);
     */
    createRit(input: CreatePitRequest, options?: RpcOptions): UnaryCall<CreatePitRequest, Sheet>;
    /**
     * @generated from protobuf rpc: GetRit(database.gui.GetRitRequest) returns (database.gui.Sheet);
     */
    getRit(input: GetRitRequest, options?: RpcOptions): UnaryCall<GetRitRequest, Sheet>;
    /**
     * @generated from protobuf rpc: UpdateRit(database.gui.UpdateRitRequest) returns (database.gui.Sheet);
     */
    updateRit(input: UpdateRitRequest, options?: RpcOptions): UnaryCall<UpdateRitRequest, Sheet>;
}
/**
 * @generated from protobuf service database.gui.RitService
 */
export class RitServiceClient implements IRitServiceClient, ServiceInfo {
    typeName = RitService.typeName;
    methods = RitService.methods;
    options = RitService.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CreateRit(database.gui.CreatePitRequest) returns (database.gui.Sheet);
     */
    createRit(input: CreatePitRequest, options?: RpcOptions): UnaryCall<CreatePitRequest, Sheet> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<CreatePitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetRit(database.gui.GetRitRequest) returns (database.gui.Sheet);
     */
    getRit(input: GetRitRequest, options?: RpcOptions): UnaryCall<GetRitRequest, Sheet> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetRitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UpdateRit(database.gui.UpdateRitRequest) returns (database.gui.Sheet);
     */
    updateRit(input: UpdateRitRequest, options?: RpcOptions): UnaryCall<UpdateRitRequest, Sheet> {
        const method = this.methods[2], opt = this._transport.mergeOptions(options);
        return stackIntercept<UpdateRitRequest, Sheet>("unary", this._transport, method, opt, input);
    }
}