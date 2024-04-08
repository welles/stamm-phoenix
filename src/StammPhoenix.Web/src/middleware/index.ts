import { useAstroI18n } from "astro-i18n"
import { sequence } from "astro/middleware"

const astroI18n = useAstroI18n(
	undefined /* config */,
	undefined /* custom formatters */,
)

export const onRequest = sequence(astroI18n)
