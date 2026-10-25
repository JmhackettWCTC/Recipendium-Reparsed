// Home page component - Quasar Material Design welcome page
export default {
    template: `
        <div class="home-page">
            <q-card class="welcome-card">
                <q-card-section>
                    <div class="text-h4 text-center q-mb-md">
                        <q-icon name="restaurant" size="48px" color="primary" class="q-mr-sm" />
                        Welcome to Recipendium
                    </div>
                    <p class="text-body1 text-center text-grey-8">
                        A combination of recipe and compendium. This website's goal is to allow people to share and store their recipes for the world to see. We hope you enjoy it!
                    </p>
                </q-card-section>
                <q-card-actions align="center" class="q-pa-md">
                    <q-btn
                        color="primary"
                        label="View Recipes"
                        icon="list"
                        unelevated
                        size="lg"
                        class="q-mr-md"
                        @click="navigateTo(recipesUrl)"
                    />
                    <q-btn
                        color="positive"
                        label="Add New Recipe"
                        icon="add_circle"
                        unelevated
                        size="lg"
                        @click="navigateTo(addRecipeUrl)"
                    />
                </q-card-actions>
            </q-card>
        </div>
    `,
    data() {
        return {
            recipesUrl: '/Recipe/Index',
            addRecipeUrl: '/Recipe/Create'
        };
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        }
    }
};
