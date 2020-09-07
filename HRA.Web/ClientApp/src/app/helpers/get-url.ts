import { environment } from 'src/environments/environment';

export function getApiUrl(endpoint: string): string {
    return `${environment.apiUrl}${endpoint}`;
  }
