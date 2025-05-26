
# 📊 Bank Marketing Term Deposit Prediction

This notebook analyzes a real dataset from a Portuguese bank's telemarketing campaign to build a model predicting whether a client will subscribe to a term deposit.

---

## 1. Exploratory Data Analysis (EDA) 🕵️‍♂️ (20 points)

### 🔹 Load Data

```python
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns

# Load dataset
df = pd.read_csv("bank_marketing.csv", delimiter=';')

# Show dataset overview
print(df.info())
display(df.describe())
```

### 🔹 Target Variable Distribution

```python
sns.set(style="whitegrid")
plt.figure(figsize=(6, 4))
sns.countplot(x='target', data=df, palette='Set2')
plt.title("Distribution of Term Deposit Subscription")
plt.xlabel("Subscribed")
plt.ylabel("Count")
plt.tight_layout()
plt.show()
```

### 🔹 Correlation Heatmap

```python
numeric_features = df.select_dtypes(include=['int64'])
plt.figure(figsize=(10, 6))
sns.heatmap(numeric_features.corr(), annot=True, cmap='coolwarm', fmt=".2f")
plt.title("Correlation Heatmap of Numeric Features")
plt.tight_layout()
plt.show()
```

### 🔹 Boxplot: Balance vs Target

```python
plt.figure(figsize=(8, 5))
sns.boxplot(x='target', y='balance', data=df)
plt.title("Account Balance by Subscription Status")
plt.tight_layout()
plt.show()
```

---

## 2. Model Selection and Training 🤖 (15 points)

We will use two models:
- **Logistic Regression** (simple and interpretable)
- **Random Forest Classifier** (handles non-linearities, good for tabular data)

```python
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import LabelEncoder
from sklearn.linear_model import LogisticRegression
from sklearn.ensemble import RandomForestClassifier

# Encode categorical variables
df_encoded = df.copy()
for col in df_encoded.select_dtypes(include='object').columns:
    df_encoded[col] = LabelEncoder().fit_transform(df_encoded[col])

# Features and target
X = df_encoded.drop("target", axis=1)
y = df_encoded["target"]

# Train-test split
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Train models
lr = LogisticRegression(max_iter=1000)
rf = RandomForestClassifier(random_state=42)

lr.fit(X_train, y_train)
rf.fit(X_train, y_train)
```

---

## 3. Hyperparameter Tuning ⚙️ (5 points)

We’ll use `GridSearchCV` for tuning the Random Forest.

```python
from sklearn.model_selection import GridSearchCV

param_grid = {
    'n_estimators': [50, 100],
    'max_depth': [None, 10, 20]
}

grid = GridSearchCV(RandomForestClassifier(random_state=42), param_grid, cv=3, scoring='accuracy')
grid.fit(X_train, y_train)
best_rf = grid.best_estimator_
```

---

## 4. Model Evaluation 📈 (10 points)

Evaluate with **Accuracy**, **Precision**, **Recall**, **F1 Score**.

```python
from sklearn.metrics import classification_report, accuracy_score

# Logistic Regression evaluation
print("Logistic Regression:")
y_pred_lr = lr.predict(X_test)
print(classification_report(y_test, y_pred_lr))

# Tuned Random Forest evaluation
print("Tuned Random Forest:")
y_pred_rf = best_rf.predict(X_test)
print(classification_report(y_test, y_pred_rf))
```

### 🔹 Discussion

- **Random Forest** typically performs better with categorical and complex data.
- **Logistic Regression** offers simplicity and interpretability.
- **Duration**, **pdays**, and **balance** seem highly correlated with the target.

---

## ✅ Conclusion

- We built two models to predict term deposit subscription.
- Random Forest (with tuning) likely outperforms logistic regression in this task.
- This analysis helps prioritize calls, improving campaign efficiency.

