// Category Index component - Quasar Material Design category cards grid
export default {
    template: `
        <div class="category-index">
            <div v-if="categoryCount > 0" class="text-caption text-grey-8 q-mb-sm">
                Showing {{ categoryCount }} categor{{ categoryCount === 1 ? 'y' : 'ies' }}
            </div>

            <div v-if="categoryCount === 0" class="no-categories">
                <q-card flat bordered class="text-center q-pa-xl">
                    <q-icon name="category" size="64px" color="grey-5" />
                    <p class="text-body1 q-mt-md">No categories found.</p>
                    <q-btn
                        color="primary"
                        label="Add a Category"
                        icon="add"
                        unelevated
                        class="q-mt-md"
                        @click="navigateTo(addCategoryUrl)"
                    />
                </q-card>
            </div>

            <div v-if="categoryCount > 0" class="row q-col-gutter-md">
                <div v-for="category in categories" :key="category.id" class="col-12 col-md-6 col-lg-4">
                    <q-card class="category-card">
                        <q-card-section>
                            <div class="row justify-between items-center q-mb-sm">
                                <div class="text-h6">{{ category.name }}</div>
                                <q-badge color="secondary" icon="restaurant_menu" :label="category.recipeCount + ' recipes'" />
                            </div>
                            <p v-if="category.description" class="text-body2 text-grey-8">{{ category.description }}</p>
                        </q-card-section>

                        <q-separator />

                        <q-card-actions align="right">
                            <q-btn
                                flat
                                dense
                                color="secondary"
                                label="Edit"
                                icon="edit"
                                @click="navigateTo('/Category/Edit/' + category.id)"
                            />
                            <q-btn
                                flat
                                dense
                                color="negative"
                                label="Delete"
                                icon="delete"
                                @click="navigateTo('/Category/Delete/' + category.id)"
                            />
                        </q-card-actions>
                    </q-card>
                </div>
            </div>

            <!-- Add New Category button -->
            <q-btn
                unelevated
                color="primary"
                label="Add New Category"
                icon="add"
                class="q-mt-md"
                @click="navigateTo(addCategoryUrl)"
            />
        </div>
    `,
    props: {
        categoriesJson: { type: String, required: true }
    },
    data() {
        return {
            addCategoryUrl: '/Category/Create'
        };
    },
    computed: {
        categories() {
            try {
                return JSON.parse(this.categoriesJson);
            } catch (e) {
                return [];
            }
        },
        categoryCount() {
            return this.categories.length;
        }
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        }
    }
};
