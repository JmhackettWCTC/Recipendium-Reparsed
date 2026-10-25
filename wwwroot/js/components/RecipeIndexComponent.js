// Recipe Index component - Quasar Material Design recipe cards grid with filtering and sorting
export default {
    template: `
        <div class="recipe-index">
            <q-card class="filter-card q-mb-md" flat bordered>
                <q-card-section>
                    <div class="row q-col-gutter-md">
                        <div class="col-12 col-md-4">
                            <q-input
                                v-model="searchQuery"
                                label="Search recipes..."
                                outlined
                                dense
                                clearable
                                placeholder="Search by name, ingredients, description"
                                @update:model-value="onSearchDebounce"
                            >
                                <template v-slot:prepend>
                                    <q-icon name="search" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12 col-md-4">
                            <q-select
                                v-model="selectedCategory"
                                :options="categoryOptions"
                                label="Filter by Category"
                                outlined
                                dense
                                clearable
                                emit-value
                                map-options
                                @update:model-value="onFilterChange"
                            />
                        </div>
                        <div class="col-12 col-md-4">
                            <q-select
                                v-model="selectedSort"
                                :options="sortOptions"
                                label="Sort by"
                                outlined
                                dense
                                emit-value
                                map-options
                                @update:model-value="onFilterChange"
                            />
                        </div>
                    </div>
                </q-card-section>
            </q-card>

            <div v-if="recipeCount > 0" class="text-caption text-grey-8 q-mb-sm">
                Showing {{ recipeCount }} recipe{{ recipeCount === 1 ? '' : 's' }}{{ categoryText }}
            </div>

            <div v-if="recipeCount === 0" class="no-recipes">
                <q-card flat bordered class="text-center q-pa-xl">
                    <q-icon name="restaurant_menu" size="64px" color="grey-5" />
                    <p class="text-body1 q-mt-md">{{ noRecipesText }}</p>
                    <q-btn
                        color="primary"
                        label="Add a Recipe"
                        icon="add"
                        unelevated
                        class="q-mt-md"
                        @click="navigateTo(addRecipeUrl)"
                    />
                </q-card>
            </div>

            <div v-if="recipeCount > 0" class="row q-col-gutter-md">
                <div v-for="recipe in recipes" :key="recipe.id" class="col-12 col-md-6 col-lg-4">
                    <q-card class="recipe-card">
                        <!-- Recipe Image -->
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
                        <div v-else class="card__image-placeholder card-image-height" style="height: 200px; display: flex; align-items: center; justify-content: center; background: #f0f0f0;">
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
                            <q-btn
                                flat
                                dense
                                color="secondary"
                                label="Edit"
                                icon="edit"
                                @click="navigateTo('/Recipe/Edit/' + recipe.id)"
                            />
                            <q-btn
                                flat
                                dense
                                color="negative"
                                label="Delete"
                                icon="delete"
                                @click="navigateTo('/Recipe/Delete/' + recipe.id)"
                            />
                        </q-card-actions>
                    </q-card>
                </div>
            </div>
        </div>
    `,
    props: {
        recipesJson: { type: String, required: true },
        categoriesJson: { type: String, required: true },
        currentCategory: { type: String, default: '' },
        currentSort: { type: String, default: 'nameAsc' },
        currentSearch: { type: String, default: '' }
    },
    data() {
        return {
            selectedCategory: null,
            selectedSort: 'nameAsc',
            searchQuery: '',
            searchTimeout: null,
            addRecipeUrl: '/Recipe/Create'
        };
    },
    computed: {
        recipes() {
            try {
                return JSON.parse(this.recipesJson);
            } catch (e) {
                return [];
            }
        },
        categories() {
            try {
                return JSON.parse(this.categoriesJson);
            } catch (e) {
                return [];
            }
        },
        categoryOptions() {
            return this.categories.map(c => ({ label: c.name, value: c.name }));
        },
        sortOptions() {
            return [
                { label: 'Name (A-Z)', value: 'nameAsc' },
                { label: 'Name (Z-A)', value: 'nameDesc' },
                { label: 'Rating (High-Low)', value: 'ratingDesc' },
                { label: 'Rating (Low-High)', value: 'ratingAsc' },
                { label: 'Time (Shortest)', value: 'timeAsc' },
                { label: 'Time (Longest)', value: 'timeDesc' }
            ];
        },
        recipeCount() {
            return this.recipes.length;
        },
        categoryText() {
            return this.selectedCategory ? ' in ' + this.selectedCategory : '';
        },
        noRecipesText() {
            return this.selectedCategory ? 'No recipes found in this category.' : 'No recipes found.';
        }
    },
    mounted() {
        if (this.currentCategory) {
            this.selectedCategory = this.currentCategory;
        }
        this.selectedSort = this.currentSort;
        this.searchQuery = this.currentSearch;
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        },
        onSearchDebounce() {
            if (this.searchTimeout) {
                clearTimeout(this.searchTimeout);
            }
            this.searchTimeout = setTimeout(() => {
                this.applyFilters();
            }, 300);
        },
        onFilterChange() {
            this.applyFilters();
        },
        applyFilters() {
            let url = '/Recipe/Index?';
            const params = [];
            if (this.searchQuery) {
                params.push('search=' + encodeURIComponent(this.searchQuery));
            }
            if (this.selectedCategory) {
                params.push('category=' + encodeURIComponent(this.selectedCategory));
            }
            params.push('sort=' + this.selectedSort);
            url += params.join('&');
            window.location.href = url;
        }
    }
};
