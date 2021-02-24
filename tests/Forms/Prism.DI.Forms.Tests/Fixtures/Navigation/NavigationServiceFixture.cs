﻿using Prism.Common;
using Prism.DI.Forms.Tests.Mocks.ViewModels;
using Prism.DI.Forms.Tests.Mocks.Views;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Prism.DI.Forms.Tests.Fixtures.Navigation
{
    public class NavigationServiceFixture : FixtureBase
    {
        public NavigationServiceFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void ContentPage_GetsCorrectNavService()
        {
            var app = CreateMockApplication();
            app.NavigationService.NavigateAsync("XamlViewMockA");

            var mainPage = app.MainPage;
            var vm = mainPage.BindingContext as XamlViewMockAViewModel;

            Assert.IsType<XamlViewMockA>(mainPage);
            Assert.NotNull(vm);

            var correctPage = ((IPageAware)vm.NavigationService).Page == mainPage;
            Assert.True(correctPage);
        }

        [Fact]
        public void ContentPage_InNavigationPage_GetsCorrectNavService()
        {
            var app = CreateMockApplication();
            app.NavigationService.NavigateAsync("NavigationPage/XamlViewMockA");

            var mainPage = app.MainPage;
            Assert.IsType<NavigationPage>(mainPage);

            var view = mainPage.Navigation.NavigationStack[0] as XamlViewMockA;
            Assert.NotNull(view);

            var vm = view.BindingContext as XamlViewMockAViewModel;
            Assert.NotNull(vm);

            var correctPage = ((IPageAware)vm.NavigationService).Page == view;
            Assert.True(correctPage);
        }

        [Fact]
        public void TabbedPage_GetsCorrectNavService()
        {
            var app = CreateMockApplication();
            app.NavigationService.NavigateAsync("XamlTabbedViewMock");

            var tp = app.MainPage as TabbedPage;
            Assert.NotNull(tp);

            var tpVm = tp.BindingContext as XamlTabbedViewMockViewModel;
            Assert.NotNull(tpVm);

            var correctNavService = ((IPageAware)tpVm.NavigationService).Page == tp;
            Assert.True(correctNavService);
        }

        [Fact]
        public void TabbedPage_ContentPage_NestedNavigationPage_GetsCorrectNavService()
        {
            var app = CreateMockApplication();
            app.NavigationService.NavigateAsync("XamlTabbedViewMock");

            var tp = app.MainPage as TabbedPage;
            Assert.NotNull(tp);

            var tab = tp.Children[0];
            Assert.NotNull(tab);

            var navPage = tab as NavigationPage;
            Assert.NotNull(navPage);

            var view = navPage.CurrentPage;
            Assert.NotNull(view);

            var vm = view.BindingContext as XamlViewMockAViewModel;
            Assert.NotNull(vm);

            var correctPageNavService = ((IPageAware)vm.NavigationService).Page == view;
            Assert.True(correctPageNavService);
        }

        [Fact]
        public void TabbedPage_ContentPage_GetsCorrectNavService()
        {
            var app = CreateMockApplication();
            app.NavigationService.NavigateAsync("XamlTabbedViewMock");

            var tp = app.MainPage as TabbedPage;
            Assert.NotNull(tp);

            var view = tp.Children[1];
            Assert.NotNull(view);

            var vm = view.BindingContext as XamlViewMockAViewModel;
            Assert.NotNull(vm);

            var correctPageNavService = ((IPageAware)vm.NavigationService).Page == view;
            Assert.True(correctPageNavService);
        }
    }
}
