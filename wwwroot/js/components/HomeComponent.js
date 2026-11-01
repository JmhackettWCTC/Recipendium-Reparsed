// Home page component - Quasar Material Design welcome page with featured recipes
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

            <!-- Featured Recipes Section -->
            <div v-if="featuredRecipesList.length > 0" class="featured-section q-mt-xl">
                <div class="text-h5 text-center q-mb-md">
                    <q-icon name="star" color="warning" class="q-mr-sm" />
                    Featured Recipes
                </div>
                <div class="row q-col-gutter-md">
                    <div v-for="recipe in featuredRecipesList" :key="recipe.id" class="col-12 col-md-4 col-lg-4">
                        <q-card class="recipe-card">
                            <q-img
                                v-if="recipe.image"
                                :src="recipe.image"
                                :alt="recipe.name"
                                height="200px"
                                fit="cover"
                            >
                                <template v-slot:loading>
                                    <div class="absolute-full row justify-center items-center">
                                        <q-spinner-colors style="color: orange" size="40px" />
                                    </div>
                                </template>
                            </q-img>
                            <div v-else class="card__image-placeholder" style="height: 200px; display: flex; align-items: center; justify-content: center; background: #f0f0f0;">
                                <span style="font-size: 48px;">🍽️</span>
                            </div>

                            <q-card-section>
                                <div class="row justify-between items-center q-mb-sm">
                                    <div class="text-h6 ellipsis" :title="recipe.name">{{ recipe.name }}</div>
                                    <q-badge v-if="recipe.categoryName" color="primary" :label="recipe.categoryName" />
                                </div>

                                <div class="recipe-card-meta row items-center q-mb-sm">
                                    <div class="recipe-card-meta__item">
                                        <q-icon name="schedule" size="16px" color="secondary" />
                                        <span>{{ recipe.time }} mins</span>
                                    </div>
                                    <div class="recipe-card-meta__item">
                                        <q-icon name="set_meal" size="16px" color="accent" />
                                        <span>{{ recipe.servings }} servings</span>
                                    </div>
                                    <div class="recipe-card-meta__item">
                                        <q-icon name="star" size="16px" color="warning" />
                                        <span>{{ recipe.rating }} / 5</span>
                                    </div>
                                </div>

                                <p class="text-body2 text-grey-8 ellipsis-2">{{ recipe.shortDescription }}</p>
                            </q-card-section>

                            <q-separator />

                            <q-card-actions align="around">
                                <q-btn
                                    flat
                                    dense
                                    color="primary"
                                    label="Details"
                                    icon="visibility"
                                    @click="navigateTo('/Recipe/Details/' + recipe.id)"
                                />
                            </q-card-actions>
                        </q-card>
                    </div>
                </div>
            </div>
        </div>
    `,
    props: {
        featuredRecipes: { type: String, default: '[]' }
    },
    data() {
        return {
            recipesUrl: '/Recipe/Index',
            addRecipeUrl: '/Recipe/Create'
        };
    },
    computed: {
        featuredRecipesList() {
            try {
                return JSON.parse(this.featuredRecipes);
            } catch (e) {
                return [];
            }
        }
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        }
    }
};
