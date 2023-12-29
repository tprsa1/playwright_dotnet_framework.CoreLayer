using Microsoft.Playwright;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class DemoDashboardPage
    {
        private readonly IPage page;
        private readonly ILocator _addNewWidgetButton;
        private readonly ILocator _demoDashboardTitle;
        private readonly ILocator _allWidgetOptions;
        private readonly ILocator _allWidgets;
        private readonly ILocator _widgetHeading;
        private readonly ILocator _resizeButton;
        private readonly ILocator _launchLink;
        private readonly ILocator _launchTitle;
        private readonly ILocator _editButton;
        private readonly ILocator _widgetNameField;
        private readonly ILocator _saveButton;
        private readonly ILocator _widgetTypes;

        public DemoDashboardPage(IPage page)
        {
            this.page = page;
            _addNewWidgetButton = page.GetByText("Add new widget");
            _demoDashboardTitle = page.Locator("[title='DEMO DASHBOARD']");
            _allWidgetOptions = page.Locator("#modal-root .wizardControlsSection__controls-wrapper--hC20C span div");
            _allWidgets = page.Locator(".react-resizable");
            _widgetHeading = page.Locator(".widgetHeader__widget-name-block--AOAHS");
            _resizeButton = page.Locator(".react-resizable-handle");
            _launchLink = page.Locator(".defectTypeItem__label--FT0hu").First;
            _launchTitle = page.Locator(".breadcrumbs__breadcrumbs--Zv5Qn > div:nth-child(4) > span > span");
            _editButton = page.Locator(".widget__widget-header--YaPlQ > div > div.widget__common-control--AUFqI > div > div:nth-child(1) > svg > path").First;
            _widgetNameField = page.GetByPlaceholder("Enter widget name");
            _saveButton = page.GetByText("Save");
            _widgetTypes = page.Locator(".wizardControlsSection__controls-wrapper--hC20C label");
        }

        public async Task ClickOnAddNewWidgetButtonAsync()
        {
            if (_addNewWidgetButton != null)
                await _addNewWidgetButton.DispatchEventAsync("click");
        }
        public async Task ClickOnSaveButtonAsync()
        {
            if (_saveButton != null)
                await _saveButton.ClickAsync();
        }

        public async Task ClickOnEditButtonAsync()
        {
            if (_editButton != null)
                await _editButton.ClickAsync();
        }

        public async Task ClickOnAppropriateLaunchAsync()
        {
            if (_launchLink != null)
                await _launchLink.ScrollIntoViewIfNeededAsync();
                await _launchLink.ClickAsync();
        }

        public async Task<string> GetLaunchTitleAsync()
        {
            if (_launchTitle != null)
                return await _launchTitle.TextContentAsync();

            return String.Empty;
        }

        public async Task<List<string?>> GetAllWidgetsAsync()
        {
            if (_allWidgetOptions == null) return new List<string?>();
            var count = await _allWidgetOptions.CountAsync();

            var textList = new List<string?>();
            for (int i = 0; i < count; i++)
            {
                var text = await _allWidgetOptions.Nth(i).TextContentAsync();
                textList.Add(text);
            }

            return textList;
        }

        public async Task<List<string?>> GetAllWidgetTypes()
        {
            if (_widgetTypes == null) return new List<string?>();
            var count = await _widgetTypes.CountAsync();

            var textList = new List<string?>();
            for (int i = 0; i < count; i++)
            {
                var text = await _widgetTypes.Nth(i).TextContentAsync();
                textList.Add(text);
            }

            return textList;
        }

        public async Task EditWIdgetNameAsync(string widgetName)
        {
            if (_widgetNameField != null)
                await _widgetNameField.ClearAsync();
                await _widgetNameField.FillAsync(widgetName);
        }

        public  ILocator? GetWidgetHeader()
        {
            return _widgetHeading != null ? _widgetHeading : throw new NullReferenceException("Get widget Header is null");
        }

        public ILocator? GetDemoDashBoardTitle()
        {
            return _demoDashboardTitle != null ? _demoDashboardTitle : throw new NullReferenceException("Get Demo Dash Board title locator is null");
        }

        public ILocator GetOriginalWidget(string? text)
        {
            return _allWidgets.Filter(new LocatorFilterOptions { HasText = text });
        }

        public ILocator? GetResizeButton()
        {
            return _resizeButton != null ? _resizeButton : throw new NullReferenceException("Get Resize Button locator is null");
        }

    }
}
