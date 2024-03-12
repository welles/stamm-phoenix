import de from '../locales/de.json';
import en from '../locales/en.json';

interface Translations {
  [key: string]: any;
}

const resources: { [key: string]: Translations } = {
  de,
  en,
};

export function getTranslation(locale: string, keys: string[]): string {
  const translations = resources[locale] || resources['de'];
  let value: any = translations;

  for (const key of keys) {
    if (value && typeof value === 'object') {
      value = value[key];
    } else {
      return keys.join('.');
    }
  }

  return value || keys.join('.');
}
