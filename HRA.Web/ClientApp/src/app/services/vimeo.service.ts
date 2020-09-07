import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class VimeoService {

    constructor(private http: HttpClient) { }

    getEmbedLink(url) {
        const a = this.getOEmbedData(url).then(function (data) {
            return data;
        });
        return this.http.get(`https://vimeo.com/api/oembed.json?url=${url}`);

    }

    getOEmbedData(videoUrl: string) {
        const params = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};
        const element = arguments.length > 2 ? arguments[2] : undefined;
        return new Promise(function (resolve, reject) {
            let url = 'https://vimeo.com/api/oembed.json?url='.concat(encodeURIComponent(videoUrl));

            for (const param in params) {
                if (params.hasOwnProperty(param)) {
                    url += '&'.concat(param, '=').concat(encodeURIComponent(params[param]));
                }
            }

            const xhr = new XMLHttpRequest();
            xhr.open('GET', url, true);

            xhr.onload = function () {
                if (xhr.status === 404) {
                    reject(new Error('\u201C'.concat(videoUrl, '\u201D was not found.')));
                    return;
                }

                if (xhr.status === 403) {
                    reject(new Error('\u201C'.concat(videoUrl, '\u201D is not embeddable.')));
                    return;
                }

                try {
                    const json = JSON.parse(xhr.responseText); // Check api response for 403 on oembed

                    if (json.domain_status_code === 403) {
                        // We still want to create the embed to give users visual feedback
                        // createEmbed(json, element);
                        reject(new Error('\u201C'.concat(videoUrl, '\u201D is not embeddable.')));
                        return;
                    }

                    resolve(json);
                } catch (error) {
                    reject(error);
                }
            };

            xhr.onerror = function () {
                const status = xhr.status ? ' ('.concat(xhr.status.toString(), ')') : '';
                reject(new Error('There was an error fetching the embed code from Vimeo'.concat(status, '.')));
            };

            xhr.send();
        });
    }
}
