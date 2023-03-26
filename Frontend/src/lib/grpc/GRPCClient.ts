import { GrpcTransport } from '@protobuf-ts/grpc-transport';
import { ChannelCredentials } from '@grpc/grpc-js';
import {
	AccountClient,
	SessionClient,
	UserClient,
	VerificationTokenClient
} from '../protos/database.client';

const transport = new GrpcTransport({
	host: 'localhost:5000',
	channelCredentials: ChannelCredentials.createInsecure()
});

const userClient = new UserClient(transport);
const sessionClient = new SessionClient(transport);
const verificationTokenClient = new VerificationTokenClient(transport);
const accountClient = new AccountClient(transport);

export const GRPCClient = {
	user: userClient,
	session: sessionClient,
	verificationToken: verificationTokenClient,
	account: accountClient
};

export type GRPCClientType = typeof GRPCClient;
