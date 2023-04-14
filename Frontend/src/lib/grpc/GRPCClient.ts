import { GrpcTransport } from '@protobuf-ts/grpc-transport';
import { ChannelCredentials } from '@grpc/grpc-js';
import {
	AccountAuthServiceClient,
	SessionAuthServiceClient,
	UserAuthServiceClient,
	VerificationTokenAuthServiceClient
} from '../protos/database.auth.client';
import {
	UserServiceClient,
	PitServiceClient,
	RitServiceClient
} from '../protos/database.gui.client';
import { EmailServiceClient } from '../protos/mail.client';
import { SheetsServiceClient } from '../protos/sheetsEditor.client';

const dbTransport = new GrpcTransport({
	host: '26.145.102.198:6924',
	// host: 'localhost:5555',
	channelCredentials: ChannelCredentials.createInsecure()
});

const mailTransport = new GrpcTransport({
	host: 'localhost:5511',
	channelCredentials: ChannelCredentials.createInsecure()
});

const sheetsTransport = new GrpcTransport({
	host: '26.211.138.120:50053',
	channelCredentials: ChannelCredentials.createInsecure()
});

const sheetsClient = new SheetsServiceClient(sheetsTransport);

const mailClient = new EmailServiceClient(mailTransport);
const pitClient = new PitServiceClient(dbTransport);
const ritClient = new RitServiceClient(dbTransport);
const userClient = new UserServiceClient(dbTransport);
const userAuthClient = new UserAuthServiceClient(dbTransport);
const sessionClient = new SessionAuthServiceClient(dbTransport);
const verificationTokenClient = new VerificationTokenAuthServiceClient(dbTransport);
const accountClient = new AccountAuthServiceClient(dbTransport);

export const GRPCClient = {
	database: {
		auth: {
			user: userAuthClient,
			session: sessionClient,
			verificationToken: verificationTokenClient,
			account: accountClient
		},
		gui: {
			user: userClient,
			pit: pitClient,
			rit: ritClient
		}
	},
	mail: mailClient,
	sheets: sheetsClient
};

export type GRPCClientType = typeof GRPCClient;
