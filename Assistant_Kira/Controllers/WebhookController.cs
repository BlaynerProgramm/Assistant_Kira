﻿using Assistant_Kira.Models;
using Assistant_Kira.Models.Webhooks.GitLab;

using Microsoft.AspNetCore.Mvc;

using Telegram.Bot;

namespace Assistant_Kira;

[Route("webhook")]
[ApiController]
public sealed class WebhookController : ControllerBase
{
	private readonly KiraBot _kiraBot;

	public WebhookController(KiraBot kiraBot)
	{
		_kiraBot = kiraBot;
	}

	[HttpPost]
	public IActionResult GitLabWebhook([FromBody] WebhookMessage webhookMessage)
	{
		if (webhookMessage.Stages.Any(x => x.Status.Equals("failed", StringComparison.InvariantCultureIgnoreCase)))
		{
			_kiraBot.TelegramApi.SendTextMessageAsync(1548307601, webhookMessage.ToString());
		}
		return Ok();
	}
}