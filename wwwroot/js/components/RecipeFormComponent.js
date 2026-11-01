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
                <input type="hidden" name="Ingredients" :value="ingredientsString" />
                <input type="hidden" name="Instructions" :value="instructionsString" />
                <input type="hidden" name="ShortDescription" v-model="formData.shortDescription" />
                <input type="hidden" name="LongDescription" v-model="formData.longDescription" />
                <input type="hidden" name="Rating" v-model="formData.rating" />
                <input type="hidden" name="Featured" :value="formData.featured ? 'true' : 'false'" />

                <!-- Hidden inputs for sub-recipes -->
                <template v-for="(sub, index) in subRecipeList" :key="index">
                    <input type="hidden" :name="'SubRecipes[' + index + '].Name'" :value="sub.name" />
                    <input type="hidden" :name="'SubRecipes[' + index + '].Ingredients'" :value="sub.ingredientsString" />
                    <input type="hidden" :name="'SubRecipes[' + index + '].Instructions'" :value="sub.instructionsString" />
                </template>

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
                                <div v-if="categories.length === 0" class="q-pa-md">
                                    <q-banner rounded class="bg-warning text-white q-mb-md">
                                        <template v-slot:avatar>
                                            <q-icon name="warning" color="white" size="xs" />
                                        </template>
                                        No categories available. Create a category first.
                                        <q-btn
                                            flat
                                            dense
                                            color="white"
                                            label="Create Category"
                                            icon-right="arrow_forward"
                                            @click="navigateTo('/Category/Create')"
                                        />
                                    </q-banner>
                                </div>
                                <q-select
                                    v-else
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
                                <div class="flexible-input-section">
                                    <div class="row items-center q-mb-sm">
                                        <div class="col">
                                            <label class="text-subtitle2 text-grey-8">Ingredients</label>
                                            <small class="text-grey-7">Add each ingredient on its own line.</small>
                                        </div>
                                        <div class="col-auto">
                                            <q-btn
                                                color="primary"
                                                icon="add"
                                                label="Add Ingredient"
                                                size="sm"
                                                unelevated
                                                @click="addIngredient"
                                            />
                                        </div>
                                    </div>
                                    <div v-for="(ingredient, index) in ingredientList" :key="index" class="row items-center q-col-gutter-sm q-mb-sm">
                                        <div class="col-1 col-auto">
                                            <span class="text-caption text-grey-8">{{ index + 1 }}.</span>
                                        </div>
                                        <div class="col">
                                            <q-input
                                                v-model="ingredientList[index]"
                                                outlined
                                                dense
                                                placeholder="e.g., 2 cups flour"
                                            />
                                        </div>
                                        <div class="col-auto">
                                            <q-btn
                                                flat
                                                dense
                                                color="negative"
                                                icon="delete"
                                                @click="removeIngredient(index)"
                                            />
                                        </div>
                                    </div>
                                    <div v-if="ingredientList.length === 0" class="text-caption text-grey-6 q-mt-sm">
                                        No ingredients added yet. Click "Add Ingredient" to start.
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="flexible-input-section">
                                    <div class="row items-center q-mb-sm">
                                        <div class="col">
                                            <label class="text-subtitle2 text-grey-8">Instructions</label>
                                            <small class="text-grey-7">Add each step of the recipe.</small>
                                        </div>
                                        <div class="col-auto">
                                            <q-btn
                                                color="primary"
                                                icon="add"
                                                label="Add Step"
                                                size="sm"
                                                unelevated
                                                @click="addInstruction"
                                            />
                                        </div>
                                    </div>
                                    <div v-for="(instruction, index) in instructionList" :key="index" class="row items-center q-col-gutter-sm q-mb-sm">
                                        <div class="col-1 col-auto">
                                            <span class="text-caption text-grey-8">{{ index + 1 }}.</span>
                                        </div>
                                        <div class="col">
                                            <q-input
                                                v-model="instructionList[index]"
                                                type="textarea"
                                                outlined
                                                dense
                                                rows="2"
                                                placeholder="Describe this step..."
                                            />
                                        </div>
                                        <div class="col-auto">
                                            <q-btn
                                                flat
                                                dense
                                                color="negative"
                                                icon="delete"
                                                @click="removeInstruction(index)"
                                            />
                                        </div>
                                    </div>
                                    <div v-if="instructionList.length === 0" class="text-caption text-grey-6 q-mt-sm">
                                        No steps added yet. Click "Add Step" to start.
                                    </div>
                                </div>
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

                    <!-- Sub-Recipes Section -->
                    <q-card-section>
                        <div class="text-h6 q-mb-md">
                            <q-icon name="layers" color="primary" class="q-mr-sm" />
                            Sub-Recipes
                        </div>
                        <small class="text-grey-7 q-mb-md">Add sub-recipes for components made separately (e.g., dressings, sauces, marinades).</small>

                        <div v-for="(sub, index) in subRecipeList" :key="index" class="sub-recipe-card q-mb-md">
                            <q-card flat bordered>
                                <q-card-section>
                                    <div class="row items-center q-mb-md">
                                        <div class="col-10">
                                            <q-input
                                                v-model="subRecipeList[index].name"
                                                label="Sub-Recipe Name"
                                                placeholder="e.g., Vinaigrette Dressing"
                                                outlined
                                                dense
                                            />
                                        </div>
                                        <div class="col-2 text-right">
                                            <q-btn
                                                flat
                                                dense
                                                color="negative"
                                                icon="delete"
                                                label="Remove"
                                                @click="removeSubRecipe(index)"
                                            />
                                        </div>
                                    </div>

                                    <div class="row q-col-gutter-md">
                                        <div class="col-12">
                                            <div class="row items-center q-mb-sm">
                                                <div class="col">
                                                    <label class="text-subtitle2 text-grey-8">Ingredients</label>
                                                </div>
                                                <div class="col-auto">
                                                    <q-btn
                                                        color="primary"
                                                        icon="add"
                                                        label="Add"
                                                        size="sm"
                                                        unelevated
                                                        @click="addSubIngredient(index)"
                                                    />
                                                </div>
                                            </div>
                                            <div v-for="(ing, iIndex) in subRecipeList[index].ingredientList" :key="iIndex" class="row items-center q-col-gutter-sm q-mb-sm">
                                                <div class="col">
                                                    <q-input
                                                        v-model="subRecipeList[index].ingredientList[iIndex]"
                                                        outlined
                                                        dense
                                                        placeholder="e.g., 2 tbsp olive oil"
                                                    />
                                                </div>
                                                <div class="col-auto">
                                                    <q-btn
                                                        flat
                                                        dense
                                                        color="negative"
                                                        icon="delete"
                                                        @click="removeSubIngredient(index, iIndex)"
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row items-center q-mb-sm">
                                                <div class="col">
                                                    <label class="text-subtitle2 text-grey-8">Instructions</label>
                                                </div>
                                                <div class="col-auto">
                                                    <q-btn
                                                        color="primary"
                                                        icon="add"
                                                        label="Add Step"
                                                        size="sm"
                                                        unelevated
                                                        @click="addSubInstruction(index)"
                                                    />
                                                </div>
                                            </div>
                                            <div v-for="(inst, iIndex) in subRecipeList[index].instructionList" :key="iIndex" class="row items-center q-col-gutter-sm q-mb-sm">
                                                <div class="col">
                                                    <q-input
                                                        v-model="subRecipeList[index].instructionList[iIndex]"
                                                        type="textarea"
                                                        outlined
                                                        dense
                                                        rows="2"
                                                        placeholder="Describe this step..."
                                                    />
                                                </div>
                                                <div class="col-auto">
                                                    <q-btn
                                                        flat
                                                        dense
                                                        color="negative"
                                                        icon="delete"
                                                        @click="removeSubInstruction(index, iIndex)"
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </q-card-section>
                            </q-card>
                        </div>

                        <div v-if="subRecipeList.length === 0" class="text-center q-pa-md">
                            <p class="text-grey-7">No sub-recipes added yet.</p>
                        </div>

                        <q-btn
                            color="secondary"
                            icon="add"
                            label="Add Sub-Recipe"
                            unelevated
                            outline
                            @click="addSubRecipe"
                        />
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
                            <div class="col-12">
                                <q-toggle
                                    v-model="formData.featured"
                                    label="Featured — Show on home page"
                                    color="warning"
                                    icon="star"
                                    icon-right="star_border"
                                />
                                <small class="text-grey-7">Featured recipes will appear on the home page.</small>
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
                shortDescription: '',
                longDescription: '',
                rating: null,
                featured: false
            },
            ingredientList: [],
            instructionList: [],
            imageFile: null,
            subRecipeList: []
        };
    },
    computed: {
        categories() {
            try {
                return JSON.parse(this.categoriesJson);
            } catch (e) {
                console.error('Failed to parse categories:', e);
                return [];
            }
        },
        categoryOptions() {
            return this.categories.map(c => ({ label: c.name, value: c.id.toString() }));
        },
        ingredientsString() {
            return this.ingredientList.filter(i => i.trim() !== '').join('\n');
        },
        instructionsString() {
            return this.instructionList.filter(i => i.trim() !== '').join('|');
        }
    },
    watch: {
        subRecipeList: {
            deep: true,
            handler() {
                // Add computed strings to each sub-recipe for hidden inputs
                this.subRecipeList.forEach(sub => {
                    sub.ingredientsString = sub.ingredientList.filter(i => i.trim() !== '').join('\n');
                    sub.instructionsString = sub.instructionList.filter(i => i.trim() !== '').join('|');
                });
            }
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
                    shortDescription: recipe.shortDescription || '',
                    longDescription: recipe.longDescription || '',
                    rating: recipe.rating || null,
                    featured: recipe.featured || false
                };
                // Parse ingredients - support both newline and pipe separators
                if (recipe.ingredients) {
                    if (recipe.ingredients.includes('\n')) {
                        this.ingredientList = recipe.ingredients.split('\n').map(i => i.trim()).filter(i => i !== '');
                    } else {
                        this.ingredientList = recipe.ingredients.split('|').map(i => i.trim()).filter(i => i !== '');
                    }
                }
                // Parse instructions - pipe separator
                if (recipe.instructions) {
                    this.instructionList = recipe.instructions.split('|').map(i => i.trim()).filter(i => i !== '');
                }
                // Parse sub-recipes
                if (recipe.subRecipes && Array.isArray(recipe.subRecipes)) {
                    this.subRecipeList = recipe.subRecipes.map(sub => {
                        let ingredientList = [];
                        if (sub.ingredients) {
                            if (sub.ingredients.includes('\n')) {
                                ingredientList = sub.ingredients.split('\n').map(i => i.trim()).filter(i => i !== '');
                            } else {
                                ingredientList = sub.ingredients.split('|').map(i => i.trim()).filter(i => i !== '');
                            }
                        }
                        let instructionList = [];
                        if (sub.instructions) {
                            instructionList = sub.instructions.split('|').map(i => i.trim()).filter(i => i !== '');
                        }
                        return {
                            name: sub.name || '',
                            ingredientList,
                            instructionList
                        };
                    });
                }
            } catch (e) {
                console.error('Failed to parse recipe data:', e);
            }
        }
    },
    methods: {
        navigateTo(url) {
            window.location.href = url;
        },
        addIngredient() {
            this.ingredientList.push('');
        },
        removeIngredient(index) {
            this.ingredientList.splice(index, 1);
        },
        addInstruction() {
            this.instructionList.push('');
        },
        removeInstruction(index) {
            this.instructionList.splice(index, 1);
        },
        addSubRecipe() {
            this.subRecipeList.push({
                name: '',
                ingredientList: [],
                instructionList: []
            });
        },
        removeSubRecipe(index) {
            this.subRecipeList.splice(index, 1);
        },
        addSubIngredient(subIndex) {
            this.subRecipeList[subIndex].ingredientList.push('');
        },
        removeSubIngredient(subIndex, ingIndex) {
            this.subRecipeList[subIndex].ingredientList.splice(ingIndex, 1);
        },
        addSubInstruction(subIndex) {
            this.subRecipeList[subIndex].instructionList.push('');
        },
        removeSubInstruction(subIndex, instIndex) {
            this.subRecipeList[subIndex].instructionList.splice(instIndex, 1);
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
