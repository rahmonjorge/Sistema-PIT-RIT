// @generated by protobuf-ts 2.8.3
// @generated from protobuf file "database.proto" (package "database.auth", syntax proto3)
// tslint:disable
import { Account } from "./database";
import type { UnlinkAccountRequest } from "./database";
import type { AdapterAccount } from "./database";
import { VerificationToken } from "./database";
import type { UseVerificationTokenRequest } from "./database";
import type { VerificationTokenObj } from "./database";
import { Session } from "./database";
import type { DeleteSessionRequest } from "./database";
import type { UpdateSessionRequest } from "./database";
import type { GetSessionAndUserResponse } from "./database";
import type { GetSessionAndUserRequest } from "./database";
import type { SessionObj } from "./database";
import type { RpcTransport } from "@protobuf-ts/runtime-rpc";
import type { ServiceInfo } from "@protobuf-ts/runtime-rpc";
import { User } from "./database";
import type { DeleteUserRequest } from "./database";
import type { UpdateUserRequest } from "./database";
import type { GetUserByAccountRequest } from "./database";
import type { GetUserByEmailRequest } from "./database";
import type { GetUserRequest } from "./database";
import { stackIntercept } from "@protobuf-ts/runtime-rpc";
import type { BasicUserResponse } from "./database";
import type { CreateUserRequest } from "./database";
import type { UnaryCall } from "@protobuf-ts/runtime-rpc";
import type { RpcOptions } from "@protobuf-ts/runtime-rpc";
/**
 * @generated from protobuf service database.auth.User
 */
export interface IUserClient {
    /**
     * @generated from protobuf rpc: CreateUser(database.auth.CreateUserRequest) returns (database.auth.BasicUserResponse);
     */
    createUser(input: CreateUserRequest, options?: RpcOptions): UnaryCall<CreateUserRequest, BasicUserResponse>;
    /**
     * @generated from protobuf rpc: GetUser(database.auth.GetUserRequest) returns (database.auth.BasicUserResponse);
     */
    getUser(input: GetUserRequest, options?: RpcOptions): UnaryCall<GetUserRequest, BasicUserResponse>;
    /**
     * @generated from protobuf rpc: GetUserByEmail(database.auth.GetUserByEmailRequest) returns (database.auth.BasicUserResponse);
     */
    getUserByEmail(input: GetUserByEmailRequest, options?: RpcOptions): UnaryCall<GetUserByEmailRequest, BasicUserResponse>;
    /**
     * @generated from protobuf rpc: GetUserByAccount(database.auth.GetUserByAccountRequest) returns (database.auth.BasicUserResponse);
     */
    getUserByAccount(input: GetUserByAccountRequest, options?: RpcOptions): UnaryCall<GetUserByAccountRequest, BasicUserResponse>;
    /**
     * @generated from protobuf rpc: UpdateUser(database.auth.UpdateUserRequest) returns (database.auth.BasicUserResponse);
     */
    updateUser(input: UpdateUserRequest, options?: RpcOptions): UnaryCall<UpdateUserRequest, BasicUserResponse>;
    /**
     * @generated from protobuf rpc: DeleteUser(database.auth.DeleteUserRequest) returns (database.auth.BasicUserResponse);
     */
    deleteUser(input: DeleteUserRequest, options?: RpcOptions): UnaryCall<DeleteUserRequest, BasicUserResponse>;
}
/**
 * @generated from protobuf service database.auth.User
 */
export class UserClient implements IUserClient, ServiceInfo {
    typeName = User.typeName;
    methods = User.methods;
    options = User.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CreateUser(database.auth.CreateUserRequest) returns (database.auth.BasicUserResponse);
     */
    createUser(input: CreateUserRequest, options?: RpcOptions): UnaryCall<CreateUserRequest, BasicUserResponse> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<CreateUserRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetUser(database.auth.GetUserRequest) returns (database.auth.BasicUserResponse);
     */
    getUser(input: GetUserRequest, options?: RpcOptions): UnaryCall<GetUserRequest, BasicUserResponse> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetUserRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetUserByEmail(database.auth.GetUserByEmailRequest) returns (database.auth.BasicUserResponse);
     */
    getUserByEmail(input: GetUserByEmailRequest, options?: RpcOptions): UnaryCall<GetUserByEmailRequest, BasicUserResponse> {
        const method = this.methods[2], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetUserByEmailRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetUserByAccount(database.auth.GetUserByAccountRequest) returns (database.auth.BasicUserResponse);
     */
    getUserByAccount(input: GetUserByAccountRequest, options?: RpcOptions): UnaryCall<GetUserByAccountRequest, BasicUserResponse> {
        const method = this.methods[3], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetUserByAccountRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UpdateUser(database.auth.UpdateUserRequest) returns (database.auth.BasicUserResponse);
     */
    updateUser(input: UpdateUserRequest, options?: RpcOptions): UnaryCall<UpdateUserRequest, BasicUserResponse> {
        const method = this.methods[4], opt = this._transport.mergeOptions(options);
        return stackIntercept<UpdateUserRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: DeleteUser(database.auth.DeleteUserRequest) returns (database.auth.BasicUserResponse);
     */
    deleteUser(input: DeleteUserRequest, options?: RpcOptions): UnaryCall<DeleteUserRequest, BasicUserResponse> {
        const method = this.methods[5], opt = this._transport.mergeOptions(options);
        return stackIntercept<DeleteUserRequest, BasicUserResponse>("unary", this._transport, method, opt, input);
    }
}
/**
 * @generated from protobuf service database.auth.Session
 */
export interface ISessionClient {
    /**
     * @generated from protobuf rpc: CreateSession(database.auth.SessionObj) returns (database.auth.SessionObj);
     */
    createSession(input: SessionObj, options?: RpcOptions): UnaryCall<SessionObj, SessionObj>;
    /**
     * @generated from protobuf rpc: GetSessionAndUser(database.auth.GetSessionAndUserRequest) returns (database.auth.GetSessionAndUserResponse);
     */
    getSessionAndUser(input: GetSessionAndUserRequest, options?: RpcOptions): UnaryCall<GetSessionAndUserRequest, GetSessionAndUserResponse>;
    /**
     * @generated from protobuf rpc: UpdateSession(database.auth.UpdateSessionRequest) returns (database.auth.SessionObj);
     */
    updateSession(input: UpdateSessionRequest, options?: RpcOptions): UnaryCall<UpdateSessionRequest, SessionObj>;
    /**
     * @generated from protobuf rpc: DeleteSession(database.auth.DeleteSessionRequest) returns (database.auth.SessionObj);
     */
    deleteSession(input: DeleteSessionRequest, options?: RpcOptions): UnaryCall<DeleteSessionRequest, SessionObj>;
}
/**
 * @generated from protobuf service database.auth.Session
 */
export class SessionClient implements ISessionClient, ServiceInfo {
    typeName = Session.typeName;
    methods = Session.methods;
    options = Session.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CreateSession(database.auth.SessionObj) returns (database.auth.SessionObj);
     */
    createSession(input: SessionObj, options?: RpcOptions): UnaryCall<SessionObj, SessionObj> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<SessionObj, SessionObj>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: GetSessionAndUser(database.auth.GetSessionAndUserRequest) returns (database.auth.GetSessionAndUserResponse);
     */
    getSessionAndUser(input: GetSessionAndUserRequest, options?: RpcOptions): UnaryCall<GetSessionAndUserRequest, GetSessionAndUserResponse> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<GetSessionAndUserRequest, GetSessionAndUserResponse>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UpdateSession(database.auth.UpdateSessionRequest) returns (database.auth.SessionObj);
     */
    updateSession(input: UpdateSessionRequest, options?: RpcOptions): UnaryCall<UpdateSessionRequest, SessionObj> {
        const method = this.methods[2], opt = this._transport.mergeOptions(options);
        return stackIntercept<UpdateSessionRequest, SessionObj>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: DeleteSession(database.auth.DeleteSessionRequest) returns (database.auth.SessionObj);
     */
    deleteSession(input: DeleteSessionRequest, options?: RpcOptions): UnaryCall<DeleteSessionRequest, SessionObj> {
        const method = this.methods[3], opt = this._transport.mergeOptions(options);
        return stackIntercept<DeleteSessionRequest, SessionObj>("unary", this._transport, method, opt, input);
    }
}
/**
 * @generated from protobuf service database.auth.VerificationToken
 */
export interface IVerificationTokenClient {
    /**
     * @generated from protobuf rpc: CreateVerificationToken(database.auth.VerificationTokenObj) returns (database.auth.VerificationTokenObj);
     */
    createVerificationToken(input: VerificationTokenObj, options?: RpcOptions): UnaryCall<VerificationTokenObj, VerificationTokenObj>;
    /**
     * @generated from protobuf rpc: useVerificationToken(database.auth.UseVerificationTokenRequest) returns (database.auth.VerificationTokenObj);
     */
    useVerificationToken(input: UseVerificationTokenRequest, options?: RpcOptions): UnaryCall<UseVerificationTokenRequest, VerificationTokenObj>;
}
/**
 * @generated from protobuf service database.auth.VerificationToken
 */
export class VerificationTokenClient implements IVerificationTokenClient, ServiceInfo {
    typeName = VerificationToken.typeName;
    methods = VerificationToken.methods;
    options = VerificationToken.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: CreateVerificationToken(database.auth.VerificationTokenObj) returns (database.auth.VerificationTokenObj);
     */
    createVerificationToken(input: VerificationTokenObj, options?: RpcOptions): UnaryCall<VerificationTokenObj, VerificationTokenObj> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<VerificationTokenObj, VerificationTokenObj>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: useVerificationToken(database.auth.UseVerificationTokenRequest) returns (database.auth.VerificationTokenObj);
     */
    useVerificationToken(input: UseVerificationTokenRequest, options?: RpcOptions): UnaryCall<UseVerificationTokenRequest, VerificationTokenObj> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<UseVerificationTokenRequest, VerificationTokenObj>("unary", this._transport, method, opt, input);
    }
}
/**
 * @generated from protobuf service database.auth.Account
 */
export interface IAccountClient {
    /**
     * @generated from protobuf rpc: LinkAccount(database.auth.AdapterAccount) returns (database.auth.AdapterAccount);
     */
    linkAccount(input: AdapterAccount, options?: RpcOptions): UnaryCall<AdapterAccount, AdapterAccount>;
    /**
     * @generated from protobuf rpc: UnlinkAccount(database.auth.UnlinkAccountRequest) returns (database.auth.AdapterAccount);
     */
    unlinkAccount(input: UnlinkAccountRequest, options?: RpcOptions): UnaryCall<UnlinkAccountRequest, AdapterAccount>;
}
/**
 * @generated from protobuf service database.auth.Account
 */
export class AccountClient implements IAccountClient, ServiceInfo {
    typeName = Account.typeName;
    methods = Account.methods;
    options = Account.options;
    constructor(private readonly _transport: RpcTransport) {
    }
    /**
     * @generated from protobuf rpc: LinkAccount(database.auth.AdapterAccount) returns (database.auth.AdapterAccount);
     */
    linkAccount(input: AdapterAccount, options?: RpcOptions): UnaryCall<AdapterAccount, AdapterAccount> {
        const method = this.methods[0], opt = this._transport.mergeOptions(options);
        return stackIntercept<AdapterAccount, AdapterAccount>("unary", this._transport, method, opt, input);
    }
    /**
     * @generated from protobuf rpc: UnlinkAccount(database.auth.UnlinkAccountRequest) returns (database.auth.AdapterAccount);
     */
    unlinkAccount(input: UnlinkAccountRequest, options?: RpcOptions): UnaryCall<UnlinkAccountRequest, AdapterAccount> {
        const method = this.methods[1], opt = this._transport.mergeOptions(options);
        return stackIntercept<UnlinkAccountRequest, AdapterAccount>("unary", this._transport, method, opt, input);
    }
}