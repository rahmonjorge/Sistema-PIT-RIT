export const serializeNonPOJOs = <T extends object>(value: T | null): T | null => {
	return structuredClone(value);
};
