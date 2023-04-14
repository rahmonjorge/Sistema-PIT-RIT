import type { Adapter, AdapterAccount, AdapterUser } from '@auth/core/adapters';
import type { ProviderType } from '@auth/core/providers';
import type { GRPCClientType } from './GRPCClient';
import { Timestamp } from '../protos/google/protobuf/timestamp';
import { ProtoProviderType } from '../protos/database.auth';

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
			const res = await client.database.auth.user.createUser({
				email: data.email,
				name: data.name || undefined,
				image: data.image || undefined,
				emailVerified: data.emailVerified ? Timestamp.fromDate(data.emailVerified) : undefined
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
			try {
				const res = await client.database.auth.user.getUser({
					id
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
			} catch (error) {
				console.log('Error during get user');
				console.log(error);
				return null;
			}
		},

		async getUserByEmail(email) {
			try {
				const res = await client.database.auth.user.getUserByEmail({
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
			} catch (error) {
				console.log('Error during get user by email');
				console.log(error);
				return null;
			}
		},

		async getUserByAccount({ provider, providerAccountId }) {
			try {
				const res = await client.database.auth.user.getUserByAccount({
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
			} catch (error) {
				console.log('Error during get user by account');
				console.log(error);
				return null;
			}
		},

		async updateUser({ id, ...data }) {
			try {
				const res = await client.database.auth.user.updateUser({
					id,
					email: data.email,
					emailVerified: data.emailVerified ? Timestamp.fromDate(data.emailVerified) : undefined,
					name: data.name ?? '',
					image: data.image ?? ''
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
			} catch (error) {
				console.log('Error during update user');
				console.log(error);
				return null;
			}
		},

		async deleteUser(id) {
			try {
				const res = await client.database.auth.user.deleteUser({
					id
				});

				const deletedUser = res.response;

				return {
					id: deletedUser.id,
					email: deletedUser.email,
					cadastroCompleto: deletedUser.cadastroCompleto,
					emailVerified: convertTimestampToDate(deletedUser.emailVerified),
					name: deletedUser.name,
					image: deletedUser.image
				};
			} catch (error) {
				console.log('Error during delete user');
				console.log(error);
				return null;
			}
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

			const res = await client.database.auth.account.linkAccount({
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
			try {
				const res = await client.database.auth.account.unlinkAccount({
					provider,
					providerAccountId
				});

				const account = res.response;

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
			} catch (error) {
				console.log('Error during unlink account');
				console.log(error);
				return undefined;
			}
		},

		async getSessionAndUser(sessionToken) {
			try {
				const res = await client.database.auth.session.getSessionAndUser({
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
			} catch (error) {
				console.log('Error during get session and user');
				console.log(error);
				return null;
			}
		},
		async createSession(data) {
			const expires = Timestamp.fromDate(data.expires);

			const res = await client.database.auth.session.createSession({
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
			try {
				let res;

				if (data.expires) {
					res = await client.database.auth.session.updateSession({
						userId: data.userId,
						sessionToken: data.sessionToken,
						expires: Timestamp.fromDate(data.expires)
					});
				} else {
					res = await client.database.auth.session.updateSession({
						userId: data.userId,
						sessionToken: data.sessionToken
					});
				}

				const session = res.response;

				return {
					userId: session.userId,
					sessionToken: session.sessionToken,
					expires: convertTimestampToDate(session.expires) as Date
				};
			} catch (error) {
				console.log('Error during update session');
				console.log(error);
				return null;
			}
		},

		async deleteSession(sessionToken) {
			try {
				const res = await client.database.auth.session.deleteSession({
					sessionToken
				});

				const deletedSession = res.response;

				return {
					userId: deletedSession.userId,
					sessionToken: deletedSession.sessionToken,
					expires: convertTimestampToDate(deletedSession.expires) as Date
				};
			} catch (error) {
				console.log('Error during delete session');
				console.log(error);
				return null;
			}
		},

		async createVerificationToken(data) {
			const res = await client.database.auth.verificationToken.createVerificationToken({
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
			const res = await client.database.auth.verificationToken.useVerificationToken({
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
