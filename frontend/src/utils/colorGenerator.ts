/**
 * Generates an array of random HEX color codes.
 * @param length - Number of unique colors to generate.
 * @returns An array of random HEX color strings.
 */
export function generateHexColors(length: number): string[] {
    const colors = new Set<string>()
    const maxHex = Math.pow(256, 3) - 1

    while (colors.size < length) {
        const color =
            "#" +
            Math.floor(Math.random() * maxHex)
                .toString(16)
                .padStart(6, "0")

        colors.add(color)
    }

    return Array.from(colors)
}
