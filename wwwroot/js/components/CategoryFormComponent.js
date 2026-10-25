// Category Form component - Quasar Material Design form for creating/editing categories
export default {
    template: `
        <div class="category-form-container">
            <q-card class="form-card" flat bordered>
                <!-- Hidden inputs for ASP.NET model binding -->
                <input type="hidden" name="Name" :value="formData.name" />
                <input type="hidden" name="Description" :value="formData.description" />
                <input type="hidden" name="Slug" :value="formData.slug" />
                <input type="hidden" name="Image" :value="formData.image" />

                <q-card-section>
                    <div class="row q-col-gutter-md">
                        <div class="col-12">
                            <q-input
                                v-model="formData.name"
                                label="Category Name"
                                placeholder="e.g., Breakfast, Lunch, Dinner"
                                outlined
                                dense
                                :rules="[val => !!val || 'Name is required']"
                            >
                                <template v-slot:prepend>
                                    <q-icon name="category" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12">
                            <q-input
                                v-model="formData.description"
                                label="Description"
                                type="textarea"
                                rows="3"
                                placeholder="Brief description of the category"
                                outlined
                            >
                                <template v-slot:prepend>
                                    <q-icon name="description" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12">
                            <q-input
                                v-model="formData.slug"
                                label="Slug"
                                placeholder="e.g., breakfast, lunch, dinner"
                                outlined
                                dense
                            >
                                <template v-slot:prepend>
                                    <q-icon name="language" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12">
                            <q-input
                                v-model="formData.image"
                                label="Image URL"
                                placeholder="URL or path to category image"
                                outlined
                                dense
                            >
                                <template v-slot:prepend>
                                    <q-icon name="image" />
                                </template>
                            </q-input>
                        </div>
                        <div class="col-12" v-if="isCreate && recipes.length > 0">
                            <q-select
                                v-model="selectedRecipes"
                                :options="recipeOptions"
                                label="Assign Recipes (optional)"
                                outlined
                                dense
                                multiple
                                use-chips
                                emit-value
                                map-options
                            >
                                <template v-slot:prepend>
                                    <q-icon name="restaurant_menu" />
                                </template>
                            </q-select>
                            <small class="text-grey-7">Hold Ctrl (Cmd on Mac) to select multiple recipes.</small>
                        </div>
                    </div>
                </q-card-section>

                <q-separator />

                <q-card-actions align="between" class="q-pa-md">
                    <q-btn
                        color="primary"
                        :label="submitLabel"
                        :icon="submitIcon"
                        unelevated
                        size="lg"
                        @click="onSubmit"
                    />
                    <q-btn
                        flat
                        color="grey-8"
                        label="Cancel"
                        icon="cancel"
                        size="lg"
                        @click="navigateTo(cancelUrl)"
                    />
                </q-card-actions>
            </q-card>

            <!-- Back button -->
            <q-btn
                flat
                dense
                color="secondary"
                label="Back to Categories"
                icon="arrow_back"
                class="q-mt-md"
                @click="navigateTo(backUrl)"
            />
        </div>
    `,
    props: {
        categoryJson: { type: String, default: 'null' },
        recipesJson: { type: String, default: '[]' },
        isCreate: { type: Boolean, default: false },
        submitLabel: { type: String, default: 'Create Category' },
        submitIcon: { type: String, default: 'add_circle' },
        cancelUrl: { type: String, default: '/Category/Index' },
        backUrl: { type: String, default: '/Category/Index' }
    },
    data() {
        return {
            formData: {
                name: '',
                description: '',
                slug: '',
                image: ''
            },
            selectedRecipes: []
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
        recipeOptions() {
            return this.recipes.map(r => ({ label: r.name, value: r.id.toString() }));
        }
    },
    mounted() {
        if (this.categoryJson && this.categoryJson !== 'null') {
            try {
                const category = JSON.parse(this.categoryJson);
                this.formData = {
                    name: category.name || '',
                    description: category.description || '',
                    slug: category.slug || '',
                    image: category.image || ''
                };
            } catch (e) {
                console.error('Failed to parse category data:', e);
            }
        }
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        },
        onSubmit() {
            // Find the parent Razor form and submit it
            const form = this.$el.closest('form');
            if (form) {
                form.submit();
            }
        }
    }
};
