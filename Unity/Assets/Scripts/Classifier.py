# Classification Task
# Importing libraries
import numpy as np
import pandas as pd
import joblib
import argparse

# Preprocessing
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import StratifiedKFold
from sklearn.model_selection import GridSearchCV, RandomizedSearchCV


# Classification methods
from sklearn.linear_model import LogisticRegression
from sklearn.naive_bayes import GaussianNB
from sklearn.neighbors import KNeighborsClassifier
from sklearn.tree import DecisionTreeClassifier

from sklearn.ensemble import RandomForestClassifier
from sklearn.ensemble import ExtraTreesClassifier
from sklearn.ensemble import AdaBoostClassifier
from sklearn.ensemble import BaggingClassifier

from sklearn.ensemble import VotingClassifier

# Evaluation Metrics
from sklearn.metrics import *

# data = pd.read_csv("Std_final_df_3clusters.csv", index_col="Unnamed: 0")
# input_variables = data.columns[data.columns!='Cluster']
# X = data[input_variables]
# y = data['Cluster']
# labels_unique = np.unique(y)
# print(X.info(), y.info())
# print(labels_unique)
# new_user = np.array(X.iloc[2])
# shape = data.shape[0]
# x = np.reshape(data, (1, shape))  
# data = pd.DataFrame(data = x, columns = ['Age', 'Education','Affective Symptoms', 
#                                             'Rumination', 'Behavioural Symptoms', 
#                                             'Anxiety Personal Impact', 'Attribution Skepticism',
#                                             'Impact Skepticism', 'Trend Skepticism',
#                                             'Response Skepticism', 'Gender', 'Marital', 'Income'])    



def prediction(x):
    data = pd.DataFrame(data = x, columns = ['Age', 'Education', 'Income', 'Male', 'Female',  'Non-binary',  'Single', 
                                             'Married', 'Divorced', 'Widowed', 'Separated','Affective Symptoms',  
                                             'Rumination', 'Behavioural Symptoms','Anxiety Personal Impact', 
                                             'Attribution Skepticism','Impact Skepticism', 'Trend Skepticism',
                                             'Response Skepticism'])
    try:
        rf_classifier = joblib.load('classifier.joblib')
    except FileNotFoundError:
        print("Classifier not found. Please train the classifier first.")
        return None
    pred = rf_classifier.predict(data)
    return pred


if __name__ == "__main__":
    arg_parse = argparse.ArgumentParser()
    arg_parse.add_argument("a")
    arguments = arg_parse.parse_args()
    prediction(arguments.a)