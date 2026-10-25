// Vue.js + Quasar app initialization
// Exports a function to create and mount a Vue app with registered components

/**
 * Create a Vue app instance with Quasar configured and mount it.
 * @param {HTMLElement|string} selector - CSS selector or HTMLElement for the mount point
 * @param {Object} components - Object mapping component names to component definitions
 *   e.g. { 'home-page': HomePageComponent }
 */
export function mountApp(selector, components) {
    const appContainer = typeof selector === 'string'
        ? document.querySelector(selector)
        : selector;

    if (!appContainer) {
        console.error('Vue mount point not found:', selector);
        return null;
    }

    if (typeof Vue === 'undefined') {
        console.error('Vue is not loaded. Make sure the Vue CDN script is included.');
        return null;
    }

    if (typeof Quasar === 'undefined') {
        console.error('Quasar is not loaded. Make sure the Quasar CDN script is included.');
        return null;
    }

    const app = Vue.createApp({});

    app.use(Quasar, {
        config: {
            brand: {
                primary: '#ff9f1c',
                secondary: '#4a3728',
                accent: '#5a9e4b',
                dark: '#2d2018',
                positive: '#5a9e4b',
                negative: '#e63946',
                info: '#5a9e4b',
                warning: '#ff9f1c',
            },
        },
    });

    // Register all provided components
    for (const [name, component] of Object.entries(components)) {
        app.component(name, component);
    }

    app.mount(appContainer);
    return app;
}
