﻿namespace BlazorRadzenMls.Services;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;

public class AuthMessageHandler(IAccessTokenProvider tokenProvider) : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider = tokenProvider;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var tokenResult = await _tokenProvider.RequestAccessToken();
        if (tokenResult.TryGetToken(out var token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        return await base.SendAsync(request, cancellationToken);
    }
}