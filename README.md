# Recipendium

## Live URL

The application is deployed and available at: https://ff-web-jh1330.azurewebsites.net/

## Three Things Fixed and Implemented

### 1. Custom Front Door (Home Page)

Replaced the default ASP.NET template home page with a custom branded welcome page for Recipendium. The home page features:

- Custom welcome component with the app name "Recipendium" and a description
- Navigation buttons to browse recipes and categories
- Custom layout with branded title, header navigation, and footer

**Files modified:** `Views/Home/Index.cshtml`, `wwwroot/js/components/HomeComponent.js`, `Views/Shared/_Layout.cshtml`

### 2. Error Handling

Implemented comprehensive error handling so visitors never see raw server errors or blank pages:

- Configured `UseExceptionHandler` in `Program.cs` to redirect unhandled exceptions to a friendly error page
- Added `UseStatusCodePagesWithReExecute` to handle 404 and other status codes gracefully
- Created `Error.cshtml` view that displays user-friendly messages based on the error type
- Controllers return `NotFound()` when a recipe or category ID doesn't exist

**Files modified:** `Program.cs`, `Controllers/HomeController.cs`, `Views/Home/Error.cshtml`

### 3. Empty List Messages

Added meaningful messages and call-to-action buttons when lists are empty, so users always have guidance on what to do next:

- Recipe index displays a "no recipes" message with an "Add a Recipe" button when the database is empty
- Category index displays a "no categories" message with an "Add a Category" button when the database is empty
- Recipe details page shows a message when ingredients are missing, with a link to edit the recipe
- Recipe details page shows a message when instructions are missing, with a link to edit the recipe
- Recipe form displays a warning banner when no categories exist, with a "Create Category" button

**Files modified:** `wwwroot/js/components/RecipeIndexComponent.js`, `wwwroot/js/components/CategoryIndexComponent.js`, `Views/Recipe/Details.cshtml`, `wwwroot/js/components/RecipeFormComponent.js`
