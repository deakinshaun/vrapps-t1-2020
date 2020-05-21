#include "VisualRecognition.h"
#include <opencv2/opencv.hpp>
#include <opencv2/dnn/dnn.hpp>
#include <opencv2/dnn/all_layers.hpp>
#ifdef ANDROID
#include <android/log.h>
#endif

extern "C" {
	// The recognition model, created during prepareModel.
	//cv::dnn::Net net;

	// The details of detected matches from the last round of doRecognise.
	cv::Mat detections;

	void prepareModel(char *dirname)
	{
		const char* protoFile = "MobileNetSSD_deploy.prototxt.txt";
		const char* modelFile = "MobileNetSSD_deploy.caffeemodel";
#ifdef ANDROID
		__android_log_print(ANDROID_LOG_ERROR, "Unity", "Unity Opening file: %s %s\n", protoFile, dirname);
#endif
		//net = cv::dnn::readNetFromCaffee
	}

	void VisualRecognition()
	{
	}

	VisualRecognition::VisualRecognition()
	{
	}

	VisualRecognition::~VisualRecognition()
	{
	}
}
