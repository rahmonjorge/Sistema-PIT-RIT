import { GRPCClient } from '$/lib/grpc/GRPCClient';
import { serializeNonPOJOs } from '$/lib/utils';
import type { PageServerLoad } from './$types';

export const load = (async ({ parent }) => {
	const { session } = await parent();

	const repsonse = GRPCClient.database.gui.user.getAnosFromUser({
		id: session?.user?.id as string
	});

	const anos = serializeNonPOJOs((await repsonse.response).anos);

	return { anos };
}) satisfies PageServerLoad;
