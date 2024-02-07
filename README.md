# Blazor.Radzen.Mls

My Blazor helper project that uses Radzen

[SITE Link on Azure](https://polite-sand-0874cf403-preview.westeurope.4.azurestaticapps.net/)
[SITE Link on GitHub](https://minkostaev.github.io/Blazor.Radzen.Mls/)

This site uses:

[Radzen.Blazor](https://blazor.radzen.com/)

[AKSoftware.Localization.MultiLanguages](https://akmultilanguages.azurewebsites.net/)

[Auth0 by Okta](https://auth0.com/)

## How to publish on GitHub

1. Add **new branch** to the repo for the site
2. In GitHub site go to **Settings -> Pages** and select the **new branch**
3. Add empty file to root repo named: **.nojekyll**
4. Add file to root repo named: **.gitattributes**
```
* binary
```
5. Publish to local **Folder**
6. Copy all content from **wwwroot** folder to repo root
7. Change file **index.html**
```
<base href="/repo name/" />
```
8. Make a copy of **index.html** with name **404.html**

[more on this video](https://www.youtube.com/watch?v=nNxII6jvPvQ&t)
