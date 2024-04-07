﻿using AutoFixture;
using AutoFixture.Xunit2;

namespace WebhookSample.Tests.Custom
{
    public class CustomFixture
    {
        public static Fixture Create()
        {
            var fixture = new Fixture();
            fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
            return fixture;
        }
    }

    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute()
          : base(() => CustomFixture.Create())
        { }
    }
}
