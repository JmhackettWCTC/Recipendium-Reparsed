// Recipe Form component - Quasar Material Design form for creating/editing recipes
// Used in both Create and Edit views; syncs form data to hidden inputs for ASP.NET model binding
export default {
    template: `
        <div class="recipe-form-container">
            <q-card class="form-card" flat bordered>
                <!-- Hidden inputs for ASP.NET model binding -->
                <input type="hidden" name="Name" v-model="formData.name" />
                <input type="hidden" name="CategoryId" v-model="formData.categoryId" />
                <input type="hidden" name="Time" v-model="formData.time" />
                <input type="hidden" name="Servings" v-model="formData.servings" />
                <input type="hidden" name="Ingredients" v-model="formData.ingredients" />
                <input type="hidden" name="Instructions" v-model="formData.instructions" />
                <input type="hidden" name="ShortDescription" v-model="formData.shortDescription" />
                <input type="hidden" name="LongDescription" v-model="formData.longDescription" />
                <input type="hidden" name="Rating" v-model="formData.rating" />

                <!-- Basic Information Section -->
                    <q-card-section>
                        <div class="text-h6 q-mb-md">
                            <q-icon name="info" color="primary" class="q-mr-sm" />
                            Basic Information
                        </div>
                        <div class="row q-col-gutter-md">
                            <div class="col-12">
                                <q-input
                                    v-model="formData.name"
                                    label="Recipe Name"
                                    placeholder="e.g., Chocolate Chip Cookies"
                                    outlined
                                    dense
                                    :rules="[val => !!val || 'Name is required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="restaurant_menu" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">A descriptive name for your recipe (2-100 characters).</small>
                            </div>
                            <div class="col-12 col-md-6">
                                <q-select
                                    v-model="formData.categoryId"
                                    :options="categoryOptions"
                                    label="Category"
                                    outlined
                                    dense
                                    emit-value
                                    map-options
                                    :rules="[val => !!val || 'Category is required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="category" />
                                    </template>
                                </q-select>
                            </div>
                            <div class="col-6 col-md-3">
                                <q-input
                                    v-model.number="formData.time"
                                    label="Time (mins)"
                                    type="number"
                                    min="1"
                                    placeholder="Minutes"
                                    outlined
                                    dense
                                    :rules="[val => val > 0 || 'Time is required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="schedule" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">Total preparation and cooking time.</small>
                            </div>
                            <div class="col-6 col-md-3">
                                <q-input
                                    v-model.number="formData.servings"
                                    label="Servings"
                                    type="number"
                                    min="1"
                                    placeholder="Servings"
                                    outlined
                                    dense
                                    :rules="[val => val > 0 || 'Servings is required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="set_meal" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">How many servings this recipe makes.</small>
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <!-- Recipe Content Section -->
                    <q-card-section>
                        <div class="text-h6 q-mb-md">
                            <q-icon name="list_alt" color="primary" class="q-mr-sm" />
                            Recipe Content
                        </div>
                        <div class="row q-col-gutter-md">
                            <div class="col-12">
                                <q-input
                                    v-model="formData.ingredients"
                                    label="Ingredients"
                                    type="textarea"
                                    rows="5"
                                    placeholder="Enter each ingredient on a new line"
                                    outlined
                                    :rules="[val => !!val || 'Ingredients are required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="checklist" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">List each ingredient on a separate line.</small>
                            </div>
                            <div class="col-12">
                                <q-input
                                    v-model="formData.instructions"
                                    label="Instructions"
                                    type="textarea"
                                    rows="8"
                                    placeholder="Enter each step separated by a pipe (|) character"
                                    outlined
                                    :rules="[val => !!val || 'Instructions are required']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="how_to_reg" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">Separate each step with a pipe (|) character.</small>
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <!-- Description Section -->
                    <q-card-section>
                        <div class="text-h6 q-mb-md">
                            <q-icon name="description" color="primary" class="q-mr-sm" />
                            Description
                        </div>
                        <div class="row q-col-gutter-md">
                            <div class="col-12">
                                <q-input
                                    v-model="formData.shortDescription"
                                    label="Short Description"
                                    placeholder="A brief summary shown on the recipe list"
                                    outlined
                                    dense
                                    :rules="[val => val && val.length >= 5 || 'Short description must be at least 5 characters']"
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="short_text" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">A short summary displayed on the recipe listing page (5-200 characters).</small>
                            </div>
                            <div class="col-12">
                                <q-input
                                    v-model="formData.longDescription"
                                    label="Long Description"
                                    type="textarea"
                                    rows="4"
                                    placeholder="A detailed description of the recipe"
                                    outlined
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="article" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">A fuller description shown on the recipe details page.</small>
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <!-- Optional Section -->
                    <q-card-section>
                        <div class="text-h6 q-mb-md">
                            <q-icon name="star_border" color="primary" class="q-mr-sm" />
                            Optional
                        </div>
                        <div class="row q-col-gutter-md">
                            <div class="col-12 col-md-6">
                                <q-input
                                    v-model.number="formData.rating"
                                    label="Rating (0-5)"
                                    type="number"
                                    min="0"
                                    max="5"
                                    step="0.1"
                                    placeholder="0 - 5"
                                    outlined
                                    dense
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="star" color="warning" />
                                    </template>
                                </q-input>
                                <small class="text-grey-7">Rate this recipe from 0 to 5 stars.</small>
                            </div>
                            <div class="col-12 col-md-6" v-if="isCreate">
                                <q-file
                                    v-model="imageFile"
                                    label="Recipe Image"
                                    accept="image/*"
                                    outlined
                                    dense
                                >
                                    <template v-slot:prepend>
                                        <q-icon name="image" />
                                    </template>
                                </q-file>
                                <small class="text-grey-7">Upload an image for your recipe (optional).</small>
                            </div>
                        </div>
                    </q-card-section>

                    <q-separator />

                    <!-- Action Buttons -->
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
                label="Back to Recipes"
                icon="arrow_back"
                class="q-mt-md"
                @click="navigateTo(backUrl)"
            />
        </div>
    `,
    props: {
        recipeJson: { type: String, default: 'null' },
        categoriesJson: { type: String, required: true },
        isCreate: { type: Boolean, default: false },
        submitLabel: { type: String, default: 'Create Recipe' },
        submitIcon: { type: String, default: 'add_circle' },
        cancelUrl: { type: String, default: '/Recipe/Index' },
        backUrl: { type: String, default: '/Recipe/Index' }
    },
    data() {
        return {
            formData: {
                name: '',
                categoryId: '',
                time: null,
                servings: null,
                ingredients: '',
                instructions: '',
                shortDescription: '',
                longDescription: '',
                rating: null
            },
            imageFile: null
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
        categoryOptions() {
            return this.categories.map(c => ({ label: c.name, value: c.id.toString() }));
        }
    },
    mounted() {
        // If editing, populate form with existing data
        if (this.recipeJson && this.recipeJson !== 'null') {
            try {
                const recipe = JSON.parse(this.recipeJson);
                this.formData = {
                    name: recipe.name || '',
                    categoryId: recipe.categoryId ? recipe.categoryId.toString() : '',
                    time: recipe.time || null,
                    servings: recipe.servings || null,
                    ingredients: recipe.ingredients || '',
                    instructions: recipe.instructions || '',
                    shortDescription: recipe.shortDescription || '',
                    longDescription: recipe.longDescription || '',
                    rating: recipe.rating || null
                };
            } catch (e) {
                console.error('Failed to parse recipe data:', e);
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
