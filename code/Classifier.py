# Classification Task
# Importing libraries
import pandas as pd
import numpy as np
import joblib
# Preprocessing
from sklearn.preprocessing import StandardScaler
# Classification methods
from sklearn.ensemble import RandomForestClassifier

# Evaluation Metrics
from sklearn.metrics import *

# data = pd.read_csv("data/Clustered_df.csv", index_col="Unnamed: 0")
# input_variables = data.columns[data.columns!='Cluster']
# X = data[input_variables]
# y = data['Cluster']
# new_user = np.array(X.iloc[2])
# x = np.reshape(new_user, (1, new_user.shape[0]))  
# data = pd.DataFrame(data = x, columns = ['Age', 'Education','Affective Symptoms', 'Rumination', 'Behavioural Symptoms', 
#                                          'Anxiety Personal Impact', 'Attribution Skepticism', 'Impact Skepticism', 
#                                          'Trend Skepticism', 'Response Skepticism', 'Male', 'Female', 'Non-binary', 
#                                          'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Income'])





def prediction(data):
    scaler = joblib.load('code/scaler')
    x = pd.DataFrame(data = data, columns = ['Age', 'Education', 'Income', 'Male', 'Female', 'Non-binary', 
                                         'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Affective Symptoms', 'Rumination', 'Behavioural Symptoms', 
                                         'Anxiety Personal Impact', 'Attribution Skepticism', 'Impact Skepticism', 
                                         'Trend Skepticism', 'Response Skepticism']) 
    

    to_be_scaled = ['Age', 'Education', 'Income', 'Affective Symptoms', 
                                            'Rumination', 'Behavioural Symptoms', 
                                            'Anxiety Personal Impact', 'Attribution Skepticism',
                                            'Impact Skepticism', 'Trend Skepticism',
                                            'Response Skepticism']
    
    not_to_be_scaled = [column for column in x.columns if column not in to_be_scaled]

    x_scaled = pd.DataFrame(scaler.transform(x[to_be_scaled]), columns=to_be_scaled)
    x = pd.concat([x_scaled[to_be_scaled[:3]], x[not_to_be_scaled], x_scaled[to_be_scaled[3:]] ], axis=1)
    print(x)
    
    try:
        rf_classifier = joblib.load('code/classifier')
    except FileNotFoundError:
        print("Classifier not found. Please train the classifier first.")
        return None
    pred = rf_classifier.predict(x)
    return pred



# print(prediction(data))
# print(y[2])