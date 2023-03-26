import type { Adapter, AdapterAccount, AdapterUser } from '@auth/core/adapters';
import type { ProviderType } from '@auth/core/providers';
import type { GRPCClientType } from './GRPCClient';
import { Timestamp } from '../protos/google/protobuf/timestamp';
import { ProtoProviderType } from '../protos/database';

function convertTimestampToDate(timestamp?: Timestamp) {
	if (!timestamp) return null;
	const { seconds, nanos } = timestamp;
	return new Date(Number(seconds) * 1000 + nanos / 1000000);
}

type OGAdapter = Omit<Adapter, 'updateUser' | 'getUserByAccount'>;

export interface CustomAdapter extends OGAdapter {
	getUserByAccount(
		providerAccountId: Pick<AdapterAccount, 'provider' | 'providerAccountId'>
	): Promise<AdapterUser | null>;
	updateUser(data: Omit<AdapterUser, 'id'> & { id: string }): Promise<AdapterUser | null>;
}

export function GRPCAdapter(client: GRPCClientType): CustomAdapter {
	return {
		async createUser(data) {
			const timestamp = Timestamp.fromDate(new Date());
			const res = await client.user.createUser({
				email: data.email,
				name: data.name ?? '',
				image: data.image ?? '',
				emailVerified: timestamp
			});

			const user = res.response;

			return {
				id: user.id,
				email: user.email,
				cadastroCompleto: user.cadastroCompleto,
				emailVerified: convertTimestampToDate(user.emailVerified),
				name: user.name,
				image: user.image
			};
		},

		async getUser(id) {
			const res = await client.user.getUser({
				id
			});

			const user = res.response;
			if (!user) return null;

			return {
				id: user.id,
				email: user.email,
				cadastroCompleto: user.cadastroCompleto,
				emailVerified: convertTimestampToDate(user.emailVerified),
				name: user.name,
				image: user.image
			};
		},

		async getUserByEmail(email) {
			const res = await client.user.getUserByEmail({
				email
			});
			const user = res.response;
			if (!user) return null;

			return {
				id: user.id,
				email: user.email,
				cadastroCompleto: user.cadastroCompleto,
				emailVerified: convertTimestampToDate(user.emailVerified),
				name: user.name,
				image: user.image
			};
		},

		async getUserByAccount({ provider, providerAccountId }) {
			const res = await client.user.getUserByAccount({
				provider,
				providerAccountId
			});

			const user = res.response;
			if (!user) return null;

			return {
				id: user.id,
				email: user.email,
				cadastroCompleto: user.cadastroCompleto,
				emailVerified: convertTimestampToDate(user.emailVerified),
				name: user.name,
				image: user.image
			};
		},

		async updateUser({ id, ...data }) {
			const res = await client.user.updateUser({
				id,
				email: data.email,
				emailVerified: data.emailVerified ? Timestamp.fromDate(data.emailVerified) : undefined,
				name: data.name ?? '',
				image: data.image ?? ''
			});

			const user = res.response;
			if (!user) return null;

			return {
				id: user.id,
				email: user.email,
				cadastroCompleto: user.cadastroCompleto,
				emailVerified: convertTimestampToDate(user.emailVerified),
				name: user.name,
				image: user.image
			};
		},

		async deleteUser(id) {
			const res = await client.user.deleteUser({
				id
			});

			const deletedUser = res.response;
			if (!deletedUser) return null;

			return {
				id: deletedUser.id,
				email: deletedUser.email,
				cadastroCompleto: deletedUser.cadastroCompleto,
				emailVerified: convertTimestampToDate(deletedUser.emailVerified),
				name: deletedUser.name,
				image: deletedUser.image
			};
		},

		async linkAccount(data) {
			let providerType: ProtoProviderType = ProtoProviderType.oauth;
			switch (data.type) {
				case 'credentials':
					providerType = ProtoProviderType.credentials;
					break;
				case 'email':
					providerType = ProtoProviderType.email;
					break;
				case 'oauth':
					providerType = ProtoProviderType.oauth;
					break;
				case 'oidc':
					providerType = ProtoProviderType.oidc;
					break;
				default:
					break;
			}

			const res = await client.account.linkAccount({
				userId: data.userId,
				type: providerType,
				provider: data.provider,
				providerAccountId: data.providerAccountId,
				refreshToken: data.refresh_token ?? '',
				accessToken: data.access_token ?? '',
				expiresIn: data.expires_in ?? 0,
				tokenType: data.token_type ?? '',
				scope: data.scope ?? '',
				idToken: data.id_token ?? '',
				sessionState: (data.session_state as string) ?? ''
			});

			const account = res.response;
			if (!account) return null;

			let resultProviderType: ProviderType = 'oauth';
			switch (account.type) {
				case ProtoProviderType.credentials:
					resultProviderType = 'credentials';
					break;
				case ProtoProviderType.email:
					resultProviderType = 'email';
					break;
				case ProtoProviderType.oauth:
					resultProviderType = 'oauth';
					break;
				case ProtoProviderType.oidc:
					resultProviderType = 'oidc';
					break;
				default:
					break;
			}

			return {
				userId: account.userId,
				type: resultProviderType,
				provider: account.provider,
				providerAccountId: account.providerAccountId,
				refreshToken: account.refreshToken,
				accessToken: account.accessToken,
				expiresIn: account.expiresIn,
				tokenType: account.tokenType,
				scope: account.scope,
				idToken: account.idToken,
				sessionState: account.sessionState
			};
		},

		async unlinkAccount({ provider, providerAccountId }) {
			const res = await client.account.unlinkAccount({
				provider,
				providerAccountId
			});

			const account = res.response;
			if (!account) return;

			let resultProviderType: ProviderType = 'oauth';
			switch (account.type) {
				case ProtoProviderType.credentials:
					resultProviderType = 'credentials';
					break;
				case ProtoProviderType.email:
					resultProviderType = 'email';
					break;
				case ProtoProviderType.oauth:
					resultProviderType = 'oauth';
					break;
				case ProtoProviderType.oidc:
					resultProviderType = 'oidc';
					break;
				default:
					break;
			}

			return {
				userId: account.userId,
				type: resultProviderType,
				provider: account.provider,
				providerAccountId: account.providerAccountId,
				refreshToken: account.refreshToken,
				accessToken: account.accessToken,
				expiresIn: account.expiresIn,
				tokenType: account.tokenType,
				scope: account.scope,
				idToken: account.idToken,
				sessionState: account.sessionState
			};
		},

		async getSessionAndUser(sessionToken) {
			const res = await client.session.getSessionAndUser({
				sessionToken
			});

			const userAndSession = res.response;
			const { user, session } = userAndSession;
			if (!user || !session) return null;

			return {
				user: {
					id: user.id,
					email: user.email,
					cadastroCompleto: user.cadastroCompleto,
					emailVerified: convertTimestampToDate(user.emailVerified),
					name: user.name,
					image: user.image
				},
				session: {
					userId: session.userId,
					sessionToken: session.sessionToken,
					expires: convertTimestampToDate(session.expires) as Date
				}
			};
		},
		async createSession(data) {
			const expires = Timestamp.fromDate(data.expires);

			const res = await client.session.createSession({
				userId: data.userId,
				sessionToken: data.sessionToken,
				expires
			});

			const session = res.response;

			return {
				userId: session.userId,
				sessionToken: session.sessionToken,
				expires: convertTimestampToDate(session.expires) as Date
			};
		},

		async updateSession(data) {
			let res;

			if (data.expires) {
				res = await client.session.updateSession({
					userId: data.userId,
					sessionToken: data.sessionToken,
					expires: Timestamp.fromDate(data.expires)
				});
			} else {
				res = await client.session.updateSession({
					userId: data.userId,
					sessionToken: data.sessionToken
				});
			}

			const session = res.response;
			if (!session) return null;

			return {
				userId: session.userId,
				sessionToken: session.sessionToken,
				expires: convertTimestampToDate(session.expires) as Date
			};
		},

		async deleteSession(sessionToken) {
			const res = await client.session.deleteSession({
				sessionToken
			});

			const deletedSession = res.response;
			if (!deletedSession) return null;

			return {
				userId: deletedSession.userId,
				sessionToken: deletedSession.sessionToken,
				expires: convertTimestampToDate(deletedSession.expires) as Date
			};
		},

		async createVerificationToken(data) {
			const res = await client.verificationToken.createVerificationToken({
				identifier: data.identifier,
				token: data.token,
				expires: Timestamp.fromDate(data.expires)
			});

			const verificationToken = res.response;

			if (!verificationToken) return null;

			return {
				identifier: verificationToken.identifier,
				token: verificationToken.token,
				expires: convertTimestampToDate(verificationToken.expires) as Date
			};
		},

		async useVerificationToken({ identifier, token }) {
			const res = await client.verificationToken.useVerificationToken({
				identifier,
				token
			});

			const verificationToken = res.response;
			if (!verificationToken) return null;

			return {
				identifier: verificationToken.identifier,
				token: verificationToken.token,
				expires: convertTimestampToDate(verificationToken.expires) as Date
			};
		}
	};
}
